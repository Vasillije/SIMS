using SIMS.Model;
using SIMS.Services;
using SIMS.UI.Dialogs;
using SIMS.UI.Dialogs.View;
using SIMS.UI.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS.UI.Dialogs.ViewModel
{
    public class UserViewModel : BaseDialogViewModel
    {
        private UserService service = new UserService();
        private List<ComboData<string>> userTypes = new List<ComboData<string>>();
        private string sort;
        public UserViewModel (UserView view) : base (view, typeof(User))
        {
            Init();
            
        }

        protected override void Init()
        {
            userTypes.Add(new ComboData<string>() { Name = "Doctor", Value = "Doctor" });
            userTypes.Add(new ComboData<string>() { Name = "Pharmacists", Value = "Pharmacists" });
            userTypes.Add(new ComboData<string>() { Name = "Patient", Value = "Patient" });


            Items = new ObservableCollection<Entity>(service.GetAll());
        }

        public List<ComboData<string>> UserTypes 
        {
            get { return userTypes; }
            set
            {
                userTypes = value;
            }
        }

        public string Sort 
        {
            get { return sort; }
            set 
            {
                string name = "";

                if (value != null) 
                {
                    name = value.Split(':')[1].Trim();
                }

                sort = name;
                OnPropertyChanged(nameof(Sort));
                DoSearch();
            }
        }

        protected override void OkCommandExecute()
        {
            base.OkCommandExecute();

            ApplicationContext.Instance.Users = new List<Entity>(Items);
            service.Save();
            Init();
        }

        protected override Entity OkAfterAddDatabase()
        {
            return SelectedItem;
        }

        protected override Entity OkAfterEditDatabase()
        {
            service.Save();
            return SelectedItem;
        }

        protected override bool DatabaseRemove(Entity item)
        {
            service.Remove(item);
            service.Save();
            return true;
        }

        protected override void DoSearch()
        {
            Items = new ObservableCollection<Entity>(service.Search(Search, Sort));
        }

    }
}
