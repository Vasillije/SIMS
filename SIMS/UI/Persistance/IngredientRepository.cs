using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.UI.Persistance
{
    public class IngredientRepository : Repository<Ingredient>
    {
        public override IEnumerable<Entity> Search (string term = "", string sort = "")
        {
            List<Entity> result = new List<Entity>();

            foreach(Entity entity in ApplicationContext.Instance.Ingredients)
            {
                if(((Ingredient)entity).Name.Contains(term) || ((Ingredient)entity).Description.Contains(term) )
                {
                    result.Add(entity);
                }
            }

            if (sort == "Name")
            {
                result = result.OrderBy(o => ((Ingredient)o).Name).ToList();
            }

      //      if (sort == "By frequency")
      //      {
      //          result = result.OrderBy(o => ((Ingredient)o).).ToList();
      //      }

            

            return result;
        }



    }
}
