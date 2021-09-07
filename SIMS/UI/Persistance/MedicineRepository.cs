using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.UI.Persistance
{
    public class MedicineRepository : Repository<Medicine>
    {

        public override IEnumerable<Entity> GetAll()
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Medicines)
            {
                if (((Medicine)entity).Deleted)
                {
                    continue;
                }

                if (ApplicationContext.Instance.User != null && ApplicationContext.Instance.User.Usertype == "Pharmacists" && ((Medicine)entity).Accepted) 
                {
                    result.Add(entity);
                }
                else if (ApplicationContext.Instance.User != null && ApplicationContext.Instance.User.Usertype == "Patient")
                {
                    result.Add(entity);
                }
                else if (ApplicationContext.Instance.User != null && ApplicationContext.Instance.User.Usertype == "Doctor")
                {
                    result.Add(entity);
                }

            }

            return result;
        }

        public override IEnumerable<Entity> Search (string term = "", string sort = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Medicines)
            {
                if(((Medicine)entity).Name.Contains(term) || ((Medicine)entity).Manufacturer.Contains(term) || ((Medicine)entity).Price.Contains(term) || ((Medicine)entity).Quantity.Contains(term) || ((Medicine)entity).Ingredient.Contains(term))
                {
                    result.Add(entity);
                }
            }

            if (sort == "Name")
            {
                result = result.OrderBy(o => ((Medicine)o).Name).ToList();
            }

            if (sort == "Price")
            {
                result = result.OrderBy(o => ((Medicine)o).Price).ToList();
            }

            if (sort == "Quantity")
            {
                result = result.OrderBy(o => ((Medicine)o).Quantity).ToList();
            }

            return result;

        }

           

    }
}
