using productuploader.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productuploader.Logic
{
    public class BusinessLogic
    {
        public static List<Product> GetProducts()
        {
            return DataBaseLogic.GetProducts();
        }
    }
}