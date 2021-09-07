using System;
using SIMS.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.CompositeCommon.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Entity Get(string id);

        IEnumerable<Entity> GetAll();
        IEnumerable<Entity> Search(string term = "", string sort = "");

        void Add(Entity entity);
        void Remove(Entity entity);
    }
}
