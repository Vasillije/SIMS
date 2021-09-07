using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Model
{
    public class Medicine : Entity
    {
        private string name;
        private string manufacturer;
        private string price;
        private string quantity;
        private string ingredient;
        private bool accepted;
        private bool answered;
        private bool deleted;
        private Dictionary<Ingredient, double> ingredients = new Dictionary<Ingredient, double>();

        public Dictionary<Ingredient, double> Ingredients 
        {
            get { return ingredients; }
            set 
            {
                ingredients = value;
                OnPropertyChanged(nameof(Ingredients));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set
            {
                manufacturer = value;
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
            }
        }
        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
            }
        }

        public string Ingredient
        {
            get { return ingredient; }
            set
            {
                ingredient = value;
            }
        }

        public bool Accepted
        {
            get { return accepted; }
            set 
            {
                accepted = value;
            }
        }

        public bool Answered
        {
            get { return answered; }
            set
            {
                answered = value;
            }
        }

        public bool Deleted
        {
            get { return deleted; }
            set
            {
                deleted = value;
            }
        }

        public override void InitExportList()
        {
            
        }

        public override string Validate(string columnName)
        {
            return string.Empty;
        }
    }
}
