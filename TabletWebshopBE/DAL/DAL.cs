using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL<TEntity> where TEntity:class
    {
        private string SqlConnection = null;

        public void SetConnectionString(string connectionString)
        {
            SqlConnection = connectionString;
        }


        //public static DataTable FillDataTable(string SQLSelectCommand, DataTable dt)
        //{
        //    OleDbConnection con = new OleDbConnection(SqlConnection);
        //    OleDbDataAdapter da = new OleDbDataAdapter(SQLSelectCommand, con);
        //    da.SelectCommand.CommandTimeout = 60000;

        //    try
        //    {
        //        con.Open();
        //        da.Fill(dt);

        //        return dt;
        //    }
        //    catch
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        if (con != null) con.Close();
        //        if (da != null) da.Dispose();
        //    }
        //}

        
        public List<TEntity> FillEntity(string SQLSelectCommand, Func<OleDbDataReader, TEntity> mapperMethod) 
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
                            entityList.Add(mapperMethod(reader));
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

        public List<TEntity> FillEntity(string SQLSelectCommand, Func<OleDbDataReader, TEntity> mapperMethod, List<OleDbParameter> parameters)
        {
            OleDbConnection con = new OleDbConnection(SqlConnection);
            OleDbCommand command = new OleDbCommand(SQLSelectCommand, con);
            command.CommandTimeout = 60000;

            List<TEntity> entityList = new List<TEntity>();

            try
            {
                if (parameters != null)
                {
                    parameters.ForEach(x => command.Parameters.Add(x));
                }

                con.Open();

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            entityList.Add(mapperMethod(reader));
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

        public bool ManageEntity(string SQLCommand, List<OleDbParameter> parameters)
        {
            OleDbConnection con = new OleDbConnection(SqlConnection);
            OleDbCommand command = new OleDbCommand(SQLCommand, con);
            command.CommandTimeout = 60000;

            try
            {
                if (parameters != null)
                {
                    parameters.ForEach(x => command.Parameters.Add(x));
                }

                con.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch 
            {

                throw;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
    }
}
