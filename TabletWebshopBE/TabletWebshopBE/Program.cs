using ProductBO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletWebshopBE
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductBase product = new ProductBase();
            List<ProductBase> products = product.GetProducts();

            Console.ReadKey();
        }
    }
}
