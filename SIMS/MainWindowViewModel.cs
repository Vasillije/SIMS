using SIMS.CompositeCommon.Enums;
using SIMS.UI.Toolbar.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ToolbarViewModel toolbarViewModel;


        public ToolbarViewModel ToolbarViewModel
        {
            get { return toolbarViewModel; }
            set { toolbarViewModel = value; }
        }
    }
}
