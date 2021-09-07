using SIMS.Model;
using SIMS.UI.Dialogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS.UI.Dialogs.View
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        UserViewModel viewModel;
        public UserView()
        {
            InitializeComponent();
            viewModel = new UserViewModel(this);
            DataContext = viewModel;
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if ((passwordBox != null))
            {
                User user = ((DataContext as UserViewModel)).SelectedItem as User;
                user.Password = passwordBox.Password;
            }
        }
    }
}
