using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabletWebshopBE
{
    public class DALDB
    {
        private static string SqlConnection = null;

        public static void SetConnectionString()
        {
            string connectionURL = ConfigurationManager.AppSettings["DBURL"];
            string password = ConfigurationManager.AppSettings["password"];

            SetConnectionString(connectionURL, password);
        }

        public static void SetConnectionString(string DBURL, string password)
        {
            SqlConnection = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{DBURL}';Persist Security Info=True;Jet OLEDB:Database Password={password}";
        }

        public enum SQLCommandType
        {
            ExecuteNonQuery, ExecuteScalar
        }

        //public static object RunSQLCommand(string SQLCommand, SQLCommandType cmdType)
        //{
        //    OleDbConnection con = new OleDbConnection(SqlConnection);
        //    OleDbCommand command = new OleDbCommand(SQLCommand, con);
        //    command.CommandTimeout = 60000;

        //    try
        //    {
        //        con.Open();
        //        switch (cmdType)
        //        {
        //            case SQLCommandType.ExecuteNonQuery:
        //                return command.ExecuteNonQuery();
        //            case SQLCommandType.ExecuteScalar:
        //                return command.ExecuteScalar();
        //            default:
        //                break;
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (con != null) con.Close();
        //    }

        //    return null;
        //}

        public static DataTable FillDataTable(string SQLSelectCommand, DataTable dt)
        {
            OleDbConnection con = new OleDbConnection(SqlConnection);
            OleDbDataAdapter da = new OleDbDataAdapter(SQLSelectCommand, con);
            da.SelectCommand.CommandTimeout = 60000;

            try
            {
                con.Open();
                da.Fill(dt);

                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null) con.Close();
                if (da != null) da.Dispose();
            }
        }

        public static List<ProductBase> FillProducts()
        {
            OleDbConnection con = new OleDbConnection(SqlConnection);
            OleDbCommand command = new OleDbCommand("SELECT * FROM TB_PRODUCT", con);
            command.CommandTimeout = 60000;

            List<ProductBase> products = new List<ProductBase>();

            try
            {
                con.Open();

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
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


                            //-->COPY THIS PART

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
                            if (!reader.IsDBNull(reader.GetOrdinal("Price")))
                                product.Price = reader.GetDouble(reader.GetOrdinal("Price"));


                            //<--COPY THIS PART

                            products.Add(product);
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null) con.Close();
            }

            return products;
        }

        public delegate object MapperDelegate(OleDbDataReader reader);
        public static List<TEntity> FillEntity<TEntity>(string SQLSelectCommand, MapperDelegate mapperMethod) where TEntity:class
        {
            OleDbConnection con = new OleDbConnection(SqlConnection);
            OleDbCommand command = new OleDbCommand(SQLSelectCommand, con);
            command.CommandTimeout = 60000;

            List<TEntity> entityList = new List<TEntity>();

            try
            {
                con.Open();

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            entityList.Add(((TEntity)mapperMethod(reader)));
                        }

                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null) con.Close();
            }

            return entityList;
        }
    }
}
