using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBO.Factory
{
    public class ProductFactory
    {
        public ProductBase GetProductTablet()
        {
            return new ProductTablet();
        }

        public ProductBase GetProductAccessory()
        {
            return new ProductAccessory();
        }

        public ProductBase GetProductTablet(string sku, string type, string name, string description, string size, string img)
        {
            return new ProductTablet(sku, type, name, description, size, img);
        }

        public ProductBase GetProductAccessory(string sku, string type, string name, string description, string color, string img)
        {
            return new ProductAccessory(sku, type, name, description, color, img);
        }
    }
}
