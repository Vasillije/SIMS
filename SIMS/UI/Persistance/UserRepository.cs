using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.UI.Persistance
{
    public class UserRepository : Repository<User>
    {
        public User GetUserWithUsernameAndPassword(string email, string passwor) 
        {
            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).Email == email && ((User)entity).Password == passwor)
                {
                    return entity as User;
                }
            }

            return null;
        }


        public override IEnumerable<Entity> Search(string term="", string sort = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in ApplicationContext.Instance.Users)
            {
                if (((User)entity).Firstname.Contains(term) || ((User)entity).Lastname.Contains(term) || ((User)entity).JMBG.Contains(term) || ((User)entity).Email.Contains(term) || ((User)entity).Cellphone.Contains(term) || ((User)entity).Usertype.Contains(term))
                {
                    result.Add(entity);
                }
            }

            if (sort == "Name") 
            {
                result = result.OrderBy(o => ((User)o).Firstname).ToList();
            }

            if (sort == "Last name")
            {
                result = result.OrderBy(o => ((User)o).Lastname).ToList();
            }

            if (sort == "User type")
            {
                result = result.OrderBy(o => ((User)o).Usertype).ToList();
            }

            return result;

        }


    }
}
