using DAL;
using ProductBO.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
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
            string command = $"INSERT INTO TB_Product ([SKU], [Type], [Name], [Description], [Img], [Size], [Color]) " +
                $"VALUES (@SKU, @Type, @Name, @Description, @Img, @Size, @Color)";

            ProductMapper mapper = new ProductMapper();
            return DAL.ManageEntity(command, mapper.GetParametersFromProduct(product));
        }

        public override bool Delete(int id)
        {
            string command = $"DELETE FROM TB_Product WHERE ID={id}";
            return DAL.ManageEntity(command, null);
        }

        public override List<ProductBase> Read(ProductBase product)
        {
            //  string filter = String.IsNullOrEmpty(product.SKU) ? "" : $"WHERE SKU='{product.SKU}'";
            //  string command = $"SELECT * FROM TB_Product {filter}";

            string command = "EXEC QxS_Product";
            List<OleDbParameter> parameters = new List<OleDbParameter>() { new OleDbParameter("@date", DateTime.Now.Date) };

            if (!String.IsNullOrEmpty(product.SKU))
            {
                command = "EXEC QxS_ProductBySKU";
                parameters.Add(new OleDbParameter("SKU", product.SKU));
            }

            ProductMapper mapper = new ProductMapper();
            return DAL.FillEntity(command, mapper.GetProductBaseFromRecord, parameters);
        }

        public override bool Update(ProductBase product)
        {
            string command = $"UPDATE TB_Product SET" +
                $" [SKU]=@SKU, [Type]=@Type, [Name]=@Name, [Description]=@Description, [Img]=@Img, [Size]=@Size, [Color]=@Color " +
                $"WHERE ID={product.Id}";
            ProductMapper mapper = new ProductMapper();
            return DAL.ManageEntity(command, mapper.GetParametersFromProduct(product));
        }
    }
}
