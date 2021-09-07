using SIMS.CompositeCommon.Enums;
using SIMS.Model;
using SIMS.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS.UI.Toolbar.ViewModel
{
    public class ToolbarViewModel : ViewModelBase
    {
        private RelayCommand userCommand;
        private RelayCommand medicineCommand;
        private RelayCommand ingredientCommand;
        private RelayCommand billCommand;
        private MainWindowViewModel mainWindowViewModel;

        public RelayCommand UserCommand
        {
            get
            {
                return userCommand ?? (userCommand = new RelayCommand(param => UserCommandExecute()));
            }
        }
        public RelayCommand MedicineCommand
        {
            get
            {
                return medicineCommand ?? (medicineCommand = new RelayCommand(param => MedicineCommandExecute()));
            }
        }
        public RelayCommand IngredientCommand
        {
            get
            {
                return ingredientCommand ?? (ingredientCommand = new RelayCommand(param => IngredientCommandExecute()));
            }
        }

        public RelayCommand BillCommand
        {
            get
            {
                return billCommand ?? (billCommand = new RelayCommand(param => BillCommandExecute()));
            }
        }

        public MainWindowViewModel MainWindowViewModel 
        {
            get { return mainWindowViewModel; }
            set
            {
                mainWindowViewModel = value;
                OnPropertyChanged(nameof(ShowUsers));
                OnPropertyChanged(nameof(ShowBills));
            }
        }

        public void UserCommandExecute()
        {
            UserView view = new UserView();
            view.ShowDialog();
        }

        public void MedicineCommandExecute()
        {
            MedicineView view = new MedicineView();
            view.ShowDialog();
        }

        public void IngredientCommandExecute()
        {
            IngredientView view = new IngredientView();
            view.ShowDialog();
        }

        public void BillCommandExecute()
        {
            BillView view = new BillView();
            view.ShowDialog();
        }


        public Visibility ShowUsers 
        {
            get 
            {
                return ApplicationContext.Instance.User != null && ApplicationContext.Instance.User.Usertype == "Doctor" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility ShowBills
        {
            get
            {
                return ApplicationContext.Instance.User != null && ApplicationContext.Instance.User.Usertype == "Patient" ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
