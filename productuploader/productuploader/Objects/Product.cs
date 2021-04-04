using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productuploader.Objects
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public Product()
        {

        }
    }
}