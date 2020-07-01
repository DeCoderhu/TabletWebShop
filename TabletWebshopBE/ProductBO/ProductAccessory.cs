using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBO
{
    public class ProductAccessory : ProductBase
    {
        public string Color { get; set; }

        public ProductAccessory()
        {

        }

        public ProductAccessory(int id, string sku, string type, string name, string description, string color, string img, double price, int unit) : base(id, sku, type, name, description, img, price, unit)
        {
            Color = color;
        }

        public override string BarCode()
        {
            return $"QRCODE:EARPHONE_{Id}";
        }
    }
}
