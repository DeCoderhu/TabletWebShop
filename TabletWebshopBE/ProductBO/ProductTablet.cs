using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBO
{
    internal class ProductTablet:ProductBase
    {
        public string Size { get; set; }

        public ProductTablet()
        {

        }

        public ProductTablet(string sku, string type, string name, string description, string size, string img):base(sku,  type,  name,  description,  img)
        {
            Size = size;
        }
    }

}
