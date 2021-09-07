using SIMS.CompositeCommon.Enums;
using SIMS.Model;
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
        private List<ComboData<Medicine>> medicines = new List<ComboData<Medicine>>();
        private Medicine selectedMedicine;
        private double quantity = 0;
        private CompositeCommon.Enums.RelayCommand addMedicine;
        private MedicineRepository medicineRepository = new MedicineRepository();

        public BillViewModel(BillView view) : base(view, typeof(Bill))
        {
            Init();
        }

        public override Entity SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    OnPropertyChanged(nameof(Data));
                }
            }
        }

        public List<TableData> Data
        {
            get
            {
                List<TableData> data = new List<TableData>();

                if (SelectedItem == null)
                {
                    return data;
                }

                foreach (KeyValuePair<Medicine, double> pair in ((Bill)SelectedItem).Medicines)
                {
                    data.Add(new TableData() { Name = pair.Key.Name, Value = pair.Value.ToString() });
                }


                return data;
            }
        }

        public CompositeCommon.Enums.RelayCommand AddMedicine
        {
            get
            {
                return addMedicine ?? (addMedicine = new RelayCommand(param => AddMedicineExecute(), param => AddMedicineCanExecute()));
            }
        }

        public void AddMedicineExecute()
        {
            if (((Bill)SelectedItem).Medicines.ContainsKey(SelectedMedicine))
            {
                ((Bill)SelectedItem).Medicines[SelectedMedicine] += quantity;
            }
            else
            {
                ((Bill)SelectedItem).Medicines.Add(SelectedMedicine, Quantity);
            }

            double price = 0;

            foreach (KeyValuePair<Medicine, double> pain in ((Bill)SelectedItem).Medicines) 
            {
                price += double.Parse(pain.Key.Price) * pain.Value;
            }

            ((Bill)SelectedItem).Totalprice = (float)price;

            ApplicationContext.Instance.Save();
            OnPropertyChanged(nameof(Data));
        }

        public bool AddMedicineCanExecute()
        {
            return SelectedItem != null && selectedMedicine != null && quantity > 0;
        }

        public List<ComboData<Medicine>> Medicines
        {
            get { return medicines; }
            set
            {
                medicines = value;
            }
        }

        public Medicine SelectedMedicine
        {
            get
            {
                return selectedMedicine;
            }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged(nameof(SelectedMedicine));
            }
        }

        public double Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(repository.GetAll());

            foreach (Medicine medicine in medicineRepository.GetAll())
            {
                medicines.Add(new ComboData<Medicine>() { Name = medicine.Name, Value = medicine });
            }
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
