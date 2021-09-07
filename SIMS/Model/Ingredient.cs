using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Model
{
    public class Ingredient : Entity
    {
        private string name;
        private string description;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public string Description
        {
            get { return description; }
            set 
            {
                description = value;
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
