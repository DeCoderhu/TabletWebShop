using ProductBO;
using ProductBO.Factory;
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
            ProductFactory productFactory = new ProductFactory();
            ProductBase product = productFactory.GetProductTablet();

            // ADD
            // ProductBase product = productFactory.GetProductTablet("SKUTEST","tablet","Test","Test_desc","8col","testimg");
            // product.AddProduct();

            // GET
            product.SKU = "TB1234";
            List<ProductBase> products = product.GetProducts();


            // UPDATE
            products[0].Description += "_UPD";
            products[0].UpdateProduct();


            // DELETE
            products[0].RemoveProduct();
           

            Console.ReadKey();
        }
    }
}
