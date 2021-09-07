using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.UI.Persistance
{
    public class BillRepository : Repository<Bill>
    {
        public override IEnumerable<Entity> Search (string term = "", string sort = "")
        {
            List<Entity> result = new List<Entity>();
            foreach(Entity entity in ApplicationContext.Instance.Bills)
            {
                if(((Bill)entity).Pharmacist.Contains(term) || ((Bill)entity).Dateandtime.Contains(term))
                {
                    result.Add(entity);
                }
            }
            return result;
        }



    }
}
