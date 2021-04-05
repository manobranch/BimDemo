using productuploader.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productuploader.Logic
{
    public class DataBaseLogic
    {
        private static bimdemodbEntities GetDbContext()
        {
            return new bimdemodbEntities();
        }

        public static List<Product> GetProducts()
        {
            using (var db = GetDbContext())
            {
                return (from hits in db.Articles
                        select hits).Select(a => new Product
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Price = a.Price,
                            Description = a.Description,
                            ImagePath = a.ImagePath
                        }).ToList();
            }
        }

        public static void InsertProduct(Product product)
        {
            using (var db = GetDbContext())
            {
                var newDbProduct = new Article
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ImagePath = product.ImagePath
                };

                db.Articles.Add(newDbProduct);

                db.SaveChanges();
            }
        }
    }
}