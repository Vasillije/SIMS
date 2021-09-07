using SIMS.UI.Components.Login.View;
using SIMS.UI.Toolbar.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainViewModel = new MainWindowViewModel();
            DataContext = mainViewModel;
            LoginView view = new LoginView(DataContext as MainWindowViewModel);

            
            view.ShowDialog();

            ((ToolbarViewModel)toolbar.DataContext).MainWindowViewModel = mainViewModel;
            mainViewModel.ToolbarViewModel = (ToolbarViewModel)toolbar.DataContext;

        }
    }
}
