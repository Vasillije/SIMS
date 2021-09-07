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
        private User user;

        public static ApplicationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationContext();
                    instance.Load();
                }
                return instance;
            }
        }

        public User User 
        {
            get { return user; }
            set 
            {
                user = value;
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

        public void Load() 
        {
            LoadUser();
            LoadIngredient();
            LoadMedicine();
            LoadBills();
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
                user.ID = data[0];
                user.JMBG = data[1];
                user.Email = data[2];
                user.Password = data[3];
                user.Firstname = data[4];
                user.Lastname = data[5];
                user.Cellphone = data[6];
                user.Usertype = data[7];
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
                medicine.ID = data[0];
                medicine.Name = data[1];
                medicine.Manufacturer = data[2];
                medicine.Price = data[3];
                medicine.Quantity = data[4];
                medicine.Ingredient = data[5];
                medicine.Accepted = bool.Parse(data[6]);
                medicine.Answered = bool.Parse(data[7]);
                medicine.Deleted = bool.Parse(data[8]);

                string[] ingData = data[9].Split(',');

                foreach (string ingD in ingData) 
                {
                    string[] values = ingD.Split(';');

                    if (values.Length < 2) 
                    {
                        continue;
                    }

                    Ingredient ing = GetIngredientById(values[0]);

                    medicine.Ingredients.Add(ing, double.Parse(values[1]));
                }

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
                ingredient.ID = data[0];
                ingredient.Name = data[1];
                ingredient.Description = data[2];

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

            StreamReader reader = new StreamReader("bills.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');
                Bill bill = new Bill();

                bill.ID = data[0];
                bill.Pharmacist = data[1];
                bill.Dateandtime = data[2];
                bill.Totalprice = float.Parse(data[3]);

                string[] ingData = data[4].Split(',');

                foreach (string ingD in ingData)
                {
                    string[] values = ingD.Split(';');

                    if (values.Length < 2)
                    {
                        continue;
                    }

                    Medicine ing = GetMedicineById(values[0]);

                    bill.Medicines.Add(ing, double.Parse(values[1]));
                }

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

                    string id = entity.ID == null || entity.ID == string.Empty ? NextId(users).ToString() : entity.ID;

                    line += id + "|";
                    line += ((User)entity).JMBG + "|";
                    line += ((User)entity).Email + "|";
                    line += ((User)entity).Password + "|";
                    line += ((User)entity).Firstname + "|";
                    line += ((User)entity).Lastname + "|";
                    line += ((User)entity).Cellphone + "|";
                    line += ((User)entity).Usertype + "|";

                    file.WriteLine(line);
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

                    string id = entity.ID == null || entity.ID == string.Empty ? NextId(medicines).ToString() : entity.ID;

                    line += id + "|";
                    line += ((Medicine)entity).Name + "|";
                    line += ((Medicine)entity).Manufacturer + "|";
                    line += ((Medicine)entity).Price + "|";
                    line += ((Medicine)entity).Quantity + "|";
                    line += ((Medicine)entity).Ingredient + "|";
                    line += ((Medicine)entity).Accepted + "|";
                    line += ((Medicine)entity).Answered + "|";
                    line += ((Medicine)entity).Deleted + "|";

                    string ingredeants = string.Empty;

                    foreach (KeyValuePair<Ingredient, double> pain in ((Medicine)entity).Ingredients) 
                    {
                        ingredeants += pain.Key.ID + ";" + pain.Value + ",";
                    }

                    line += ingredeants;

                    file.WriteLine(line);
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

                    string id = entity.ID == null || entity.ID == string.Empty ? NextId(ingredients).ToString() : entity.ID;

                    line += id + "|";
                    line += ((Ingredient)entity).Name + "|";
                    line += ((Ingredient)entity).Description + "|";

                    file.WriteLine(line);
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

                    string id = entity.ID == null || entity.ID == string.Empty ? NextId(bills).ToString() : entity.ID;

                    line += id + "|";
                    line += ((Bill)entity).Pharmacist + "|";
                    line += ((Bill)entity).Dateandtime + "|";
                    line += ((Bill)entity).Totalprice + "|";

                    string ingredeants = string.Empty;

                    foreach (KeyValuePair<Medicine, double> pain in ((Bill)entity).Medicines)
                    {
                        ingredeants += pain.Key.ID + ";" + pain.Value + ",";
                    }

                    line += ingredeants;

                    file.WriteLine(line);
                }
            }

        }


        public void Save() 
        {
            SaveUsers();
            SaveIngredients();
            SaveMedicines();
            SaveBills();
        }

        public Ingredient GetIngredientById(string id) 
        {
            foreach (Entity e in ingredients) 
            {
                if (e.ID == id) 
                {
                    return e as Ingredient;
                }
            }

            return null;
        }

        public Medicine GetMedicineById(string id)
        {
            foreach (Entity e in medicines)
            {
                if (e.ID == id)
                {
                    return e as Medicine;
                }
            }

            return null;
        }

        public int NextId(List<Entity> entities) 
        {
            int i = 0;

            foreach (Entity entity in entities) 
            {
                if (entity.ID == null || entity.ID == string.Empty) 
                {
                    continue;
                }


                if (int.Parse(entity.ID) > i) 
                {
                    i = int.Parse(entity.ID);
                }
            }

            return i + 1;
        }
    }
}
