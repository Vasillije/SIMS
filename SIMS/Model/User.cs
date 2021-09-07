using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.Model
{
    public class User : Entity
    {
        private string jmbg;
        private string email;
        private string password;
        private string firstname;
        private string lastname;
        private string cellphone;
        private string usertype;

        public string JMBG
        {
            get { return jmbg; }
            set
            {
                jmbg = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
            }

        }

        
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        
        }


        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
            }
        }

        public string Lastname
        {
            get { return lastname; }
            set 
            {
                lastname = value;
            }
        }

        public string Cellphone
        {
            get { return cellphone; }
            set 
            { 
                cellphone = value; 
            }
        }
        public string Usertype
        {
            get { return usertype; }
            set
            {
                usertype = value;
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
