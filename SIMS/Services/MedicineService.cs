using SIMS.Model;
using SIMS.UI.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Services
{
    public class MedicineService
    {
        private MedicineRepository repository = new MedicineRepository();

        public IEnumerable<Entity> Search(string search, string sort)
        {
            return repository.Search(search, sort);
        }

        public IEnumerable<Entity> GetAll()
        {
            return repository.GetAll();
        }

        public void Save()
        {
            repository.Save();
        }

        public void Remove(Entity entity)
        {
            repository.Remove(entity);
        }
    }
}
