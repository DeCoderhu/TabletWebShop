using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class CRUD<TEntity> where TEntity:class
    {
        public readonly DAL<TEntity> DAL = new DAL<TEntity>();

        public abstract bool Create(TEntity entity);
        public abstract List<TEntity> Read(TEntity entity);
        public abstract bool Update(TEntity entity);
        public abstract bool Delete(int id);

        public void SetConnection(string connectionString)
        {
            DAL.SetConnectionString(connectionString);
        }
    }
}
