using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Model
{
    public class Bill : Entity
    {
        private string pharmacist;
        private string dateandtime;
        private Dictionary<Medicine, int> medicines;
        private float totalprice;

        public string Pharmacist
        {
            get { return pharmacist; }
            set
            {
                pharmacist = value;
            }
        }

        
        public string Dateandtime
        {
            get { return dateandtime; }
            set
            {
                dateandtime = value;
            }
        }

        public Dictionary<Medicine, int> Medicines
        {
            get { return medicines; }
            set
            {
                medicines = value;
            }

        }

        public float Totalprice
        {
            get { return totalprice; }
            set 
            {
                totalprice = value;
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
