﻿using SIMS.Model;
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
    public class BillViewModel : BaseDialogViewModel
    {
        private BillRepository repository = new BillRepository();
        private string sort;
        public BillViewModel(BillView view) : base(view, typeof(Bill))
        {
            Init();

        }

        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(repository.GetAll());
        }

        public string Sort
        {
            get { return sort; }
            set
            {
                sort = value;
                OnPropertyChanged(nameof(Sort));
                DoSearch();
            }
        }

        protected override void OkCommandExecute()
        {
            base.OkCommandExecute();

            ApplicationContext.Instance.Bills = new List<Entity>(Items);
            repository.Save();
            Init();
        }

        protected override Entity OkAfterAddDatabase()
        {
            return SelectedItem;
        }

        protected override Entity OkAfterEditDatabase()
        {
            repository.Save();
            return SelectedItem;
        }

        protected override bool DatabaseRemove(Entity item)
        {
            repository.Remove(item);
            repository.Save();
            return true;
        }

        protected override void DoSearch()
        {
            Items = new ObservableCollection<Entity>(repository.Search(Search , Sort));
        }

    }
}
