using productuploader.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
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

            DataBaseLogic.InsertProduct(newProduct);

            QueueLogic.PutMessageOnQueue(newProduct.ImagePath);

            return true;
        }
    }
}