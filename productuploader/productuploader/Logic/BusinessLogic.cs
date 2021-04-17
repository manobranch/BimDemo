using productuploader.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace productuploader.Logic
{
    public class BusinessLogic
    {
        public static List<Product> GetProducts()
        {
            return DataBaseLogic.GetProducts();
        }

        internal static bool AddProduct(FormCollection col, HttpPostedFileBase[] file)
        {
            // Upload to blob storage
            var imagePath = BlobStorageLogic.UploadBlob(file[0]);

            // Create basic product
            Product newProduct = new Product()
            {
                Name = col["name"],
                Price = Convert.ToInt32(col["price"]),
                Description = col["description"],
                ImagePath = imagePath
            };

            // Insert to database
            DataBaseLogic.InsertProduct(newProduct);

            // Put message on queue
            QueueLogic.PutMessageOnQueue(newProduct.ImagePath);

            // Add custom event to Application Insights
            InsightsLogic.LogInfo($"Product added! {newProduct.Name}, {newProduct.Price}");

            return true;
        }

        internal static void ThrowException()
        {
            try
            {
                int zero = 0;
                var divideByZero = 4 / zero;
            }
            catch (Exception e)
            {
                InsightsLogic.LogError($"Some manual exception message. ", e);

                throw e;
            }
        }

        internal static void WebRequest()
        {
            try
            {
                WebClient client = new WebClient();
                string downloadString = client.DownloadString("https://www.hitta.se");
            }
            catch (Exception e)
            {
            }
        }
    }
}