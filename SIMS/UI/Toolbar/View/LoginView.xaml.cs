using SIMS.UI.Components.Login.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SIMS.UI.Components.Login.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel viewModel;

        public LoginView(MainWindowViewModel mainViewModel)
        {
            InitializeComponent();
            viewModel = new LoginViewModel(this, passwordBox, mainViewModel);
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if ((passwordBox != null))
            {
                ((DataContext as LoginViewModel)).Password = passwordBox.Password;
            }
        }

        public LoginViewModel ViewModel
        {
            get { return viewModel; }
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void Dispose()
        {
            this.Close();
        }
    }
}
