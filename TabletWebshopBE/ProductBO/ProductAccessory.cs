using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBO
{
    internal class ProductAccessory : ProductBase
    {
        public string Color { get; set; }

        public ProductAccessory()
        {

        }

        public ProductAccessory(string sku, string type, string name, string description, string color, string img) : base(sku, type, name, description, img)
        {
            Color = color;
        }

        public override string BarCode()
        {
            return $"QRCODE:EARPHONE_{Id}";
        }
    }
}
