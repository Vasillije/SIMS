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
        private Dictionary<Medicine, double> medicines = new Dictionary<Medicine, double>();
        private float totalprice;

        public Dictionary<Medicine, double> Medicines 
        {
            get { return medicines; }
            set 
            {
                medicines = value;
            }
        }

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
