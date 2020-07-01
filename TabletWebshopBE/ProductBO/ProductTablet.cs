using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBO
{
    public class ProductTablet:ProductBase
    {
        public string Size { get; set; }

        public ProductTablet()
        {

        }

        public ProductTablet(int id, string sku, string type, string name, string description, string size, string img, double price, int unit):base( id,  sku,  type,  name,  description,  img,  price,  unit)
        {
            Size = size;
        }
    }

}
