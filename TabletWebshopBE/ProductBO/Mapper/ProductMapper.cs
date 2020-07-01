using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBO.Mapper
{
    public class ProductMapper
    {
        public ProductBase GetProductBaseFromRecord(OleDbDataReader reader)
        {
            ProductBase product = null;
            string type = String.Empty;

            if (!reader.IsDBNull(reader.GetOrdinal("Type")))
                type = reader.GetString(reader.GetOrdinal("Type"));

            if (type.Equals("tablet"))
            {
                product = new ProductTablet();

                if (!reader.IsDBNull(reader.GetOrdinal("Size")))
                    ((ProductTablet)product).Size = reader.GetString(reader.GetOrdinal("Size"));
            }
            else if (type.Equals("accessory"))
            {
                product = new ProductAccessory();

                if (!reader.IsDBNull(reader.GetOrdinal("Color")))
                    ((ProductAccessory)product).Color = reader.GetString(reader.GetOrdinal("Color"));
            }

            if (product != null)
            {
                if (!reader.IsDBNull(reader.GetOrdinal("ID")))
                    product.Id = reader.GetInt32(reader.GetOrdinal("ID"));

                if (!reader.IsDBNull(reader.GetOrdinal("SKU")))
                    product.SKU = reader.GetString(reader.GetOrdinal("SKU"));

                product.Type = type;

                if (!reader.IsDBNull(reader.GetOrdinal("Name")))
                    product.Name = reader.GetString(reader.GetOrdinal("Name"));

                if (!reader.IsDBNull(reader.GetOrdinal("Description")))
                    product.Description = reader.GetString(reader.GetOrdinal("Description"));

                if (!reader.IsDBNull(reader.GetOrdinal("Img")))
                    product.Img = reader.GetString(reader.GetOrdinal("Img"));

                //SET! IN ProductBase
                //if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                //    product.Price = reader.GetDouble(reader.GetOrdinal("Price"));

            }
            return product;
        }

        public List<OleDbParameter> GetParametersFromProduct(ProductBase product)
        {
            List<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@SKU", product.SKU));
            parameters.Add(new OleDbParameter("@Type", product.Type));
            parameters.Add(new OleDbParameter("@Name", product.Name));
            parameters.Add(new OleDbParameter("@Description", product.Description));
            parameters.Add(new OleDbParameter("@Img", product.Img));

            if (product is ProductTablet)
            {
                parameters.Add(new OleDbParameter("@Size", ((ProductTablet)product).Size));
                parameters.Add(new OleDbParameter("@Color", DBNull.Value));
            }
            else if (product is ProductAccessory)
            {
                parameters.Add(new OleDbParameter("@Size", DBNull.Value));
                parameters.Add(new OleDbParameter("@Color", ((ProductAccessory)product).Color));
            }

            return parameters;
        }
    }
}
