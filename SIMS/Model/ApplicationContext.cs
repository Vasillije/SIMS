using SIMS.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS.Model
{
    public class ApplicationContext
    {
        private static ApplicationContext instance = null;
        private List<Entity> users = new List<Entity>();
        private List<Entity> medicines = new List<Entity>();
        private List<Entity> ingredients = new List<Entity>();
        private List<Entity> bills = new List<Entity>();

        public static ApplicationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationContext();
                }
                return instance;
            }
        }

        public List<Entity> Users
        {
            get { return users; }
            set { users = value; }
        }

        public List<Entity> Medicines
        {
            get { return medicines; }
            set { medicines = value; }
        }

        public List<Entity> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        public List<Entity> Bills
        {
            get { return bills; }
            set { bills = value; }
        }

        public void LoadUser()
        {
            List<Entity> result = new List<Entity>();

            if (!File.Exists("users.txt"))
            {
                users = result;
                return;
            }

            StreamReader reader = new StreamReader("users.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                User user = new User();
                user.JMBG = data[0];
                user.Email = data[1];
                user.Password = data[2];
                user.Firstname = data[3];
                user.Lastname = data[4];
                user.Cellphone = data[5];
                user.Usertype = data[6];
                result.Add(user);

            }

            users = result;
        }

        public void LoadMedicine()
        {
            List<Entity> result = new List<Entity>();

            if (!File.Exists("medicines.txt"))
            {
                medicines = result;
                return;
            }

            StreamReader reader = new StreamReader("medicines.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');
                
                Medicine medicine = new Medicine();
                medicine.Name = data[0];
                medicine.Manufacturer = data[1];
                medicine.Price = data[2];
                medicine.Quantity = data[3];
                medicine.Ingredient = data[4];
              //  medicine.Accepted = data[5];
              //  medicine.Deleted = data[6];
                result.Add(medicine);

            }
            medicines = result;

        }

        public void LoadIngredient()
        {
            List<Entity> result = new List<Entity>();

            if (!File.Exists("ingredients.txt"))
            {
                ingredients = result;
                return;
            }

            StreamReader reader = new StreamReader("ingredients.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                Ingredient ingredient = new Ingredient();
                ingredient.Name = data[0];
                ingredient.Description = data[1];

                result.Add(ingredient);
            }

            ingredients = result;

        }

        public void LoadBills()
        {
            List<Entity> result = new List<Entity>();

            if (!File.Exists("bills.txt"))
            {
                bills = result;
                return;
            }

            StreamReader reader = new StreamReader("bills");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');
                Bill bill = new Bill();

                bill.Pharmacist = data[0];
                bill.Dateandtime = data[1];
                //bill.Medicines = data[2];
               // bill.Totalprice = data[3];

                result.Add(bill);
            }

            bills = result; 
        }

    
        
        public List<Entity> Get(Type type)
        {
            if(type == typeof(User))
            {
                return Users;
            }

            if (type == typeof(Medicine))
            {
                return Medicines;
            }

            if (type == typeof(Ingredient))
            {
                return Ingredients;
            }

            
            return Bills;
            

        }


        public void Set(Type type, List<Entity> entities)
        {
            if(type == typeof(User))
            {
                Users = entities;
                return;
            }

            if (type == typeof(Medicine))
            {
                Medicines = entities;
                return;
            }

            if (type == typeof(Ingredient))
            {
                Ingredients = entities;
                return;
            }

            
            Bills = entities;
            return;
            


        }

        public void SaveUsers()
        {
            if (users==null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("users.txt"))
            {
                foreach (Entity entity in users)
                {
                    string line = string.Empty;

                    line += ((User)entity).ID + "|";
                    line += ((User)entity).JMBG + "|";
                    line += ((User)entity).Email + "|";
                    line += ((User)entity).Password + "|";
                    line += ((User)entity).Firstname + "|";
                    line += ((User)entity).Lastname + "|";
                    line += ((User)entity).Cellphone + "|";
                    line += ((User)entity).Usertype + "|";

                }
            }

        }

        public void SaveMedicines()
        {
            if (medicines == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("medicines.txt"))
            {
                foreach (Entity entity in medicines)
                {
                    string line = string.Empty;

                    line += ((Medicine)entity).ID + "|";
                    line += ((Medicine)entity).Name + "|";
                    line += ((Medicine)entity).Manufacturer + "|";
                    line += ((Medicine)entity).Price + "|";
                    line += ((Medicine)entity).Quantity + "|";
                    line += ((Medicine)entity).Ingredient + "|";
                    line += ((Medicine)entity).Accepted + "|";
                    line += ((Medicine)entity).Deleted + "|";
                    
                }
            }

        }

        public void SaveIngredients()
        {
            if (ingredients == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("ingredients.txt"))
            {
                foreach (Entity entity in ingredients)
                {
                    string line = string.Empty;

                    line += ((Ingredient)entity).ID + "|";
                    line += ((Ingredient)entity).Name + "|";
                    line += ((Ingredient)entity).Description + "|";
                   
                }
            }

        }

        public void SaveBills()
        {
            if (bills == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("bills.txt"))
            {
                foreach (Entity entity in bills)
                {
                    string line = string.Empty;

                    line += ((Bill)entity).ID + "|";
                    line += ((Bill)entity).Pharmacist + "|";
                    line += ((Bill)entity).Dateandtime + "|";
                    line += ((Bill)entity).Medicines + "|";
                    line += ((Bill)entity).Totalprice + "|";

                }
            }

        }


        public void Save() 
        {
           
        }

    }
}
