using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarvestHaven.Repository
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
    }
}
