using DAL;
using ProductBO.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBO.DAO
{
    internal class ProductDAO : CRUD<ProductBase>
    {
        public ProductDAO()
        {
            string connectionURL = ConfigurationManager.AppSettings["DBURL"];
            string password = ConfigurationManager.AppSettings["password"];

            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{connectionURL}';Persist Security Info=True;Jet OLEDB:Database Password={password}";

            SetConnection(connectionString);
        }

        public override bool Create(ProductBase product)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ProductBase> Read(ProductBase product)
        {
            string filter = String.IsNullOrEmpty(product.SKU) ? "" : $"WHERE SKU='{product.SKU}'";
            string command = $"SELECT * FROM TB_Product {filter}";

            ProductMapper mapper = new ProductMapper();
            return DAL.FillEntity(command, mapper.GetProductBaseFromRecord);
        }

        public override bool Update(ProductBase product)
        {
            throw new NotImplementedException();
        }
    }
}
