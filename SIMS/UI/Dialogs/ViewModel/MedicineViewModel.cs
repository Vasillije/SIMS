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
    public class MedicineViewModel : BaseDialogViewModel
    {
        private MedicineRepository repository = new MedicineRepository();
        private IngredientRepository ingredientRepository = new IngredientRepository();
        private string sort;
        private List<ComboData<Ingredient>> ingredients = new List<ComboData<Ingredient>>();
        private Ingredient selectedIngredient;
        private double quantity = 0;
        private RelayCommand addIngredient;

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

                foreach (KeyValuePair<Ingredient, double> pair in ((Medicine)SelectedItem).Ingredients) 
                {
                    data.Add(new TableData() { Name = pair.Key.Name, Value = pair.Value.ToString() });
                }


                return data;
            }
        }


        public MedicineViewModel(MedicineView view) : base(view, typeof(Medicine))
        {
            Init();

            foreach (Ingredient ingredient in ingredientRepository.GetAll()) 
            {
                ingredients.Add(new ComboData<Ingredient>() { Name = ingredient.Name, Value = ingredient });
            }

        }

        public RelayCommand AddIngredient 
        {
            get 
            {
                return addIngredient ?? (addIngredient = new RelayCommand(param => AddIngredientExecute(), param => AddIngredientCanExecute() ));
            }
        }

        public void AddIngredientExecute() 
        {
            if (((Medicine)SelectedItem).Ingredients.ContainsKey(SelectedIngredient))
            {
                ((Medicine)SelectedItem).Ingredients[SelectedIngredient] += quantity;
            }
            else 
            {
                ((Medicine)SelectedItem).Ingredients.Add(SelectedIngredient, Quantity);
            }

            ApplicationContext.Instance.Save();
            OnPropertyChanged(nameof(Data));
        }

        public bool AddIngredientCanExecute() 
        {
            return SelectedItem != null && selectedIngredient != null && quantity > 0;
        }

        public List<ComboData<Ingredient>> Ingredients 
        {
            get { return ingredients; }
            set 
            {
                ingredients = value;
            }
        }

        public Ingredient SelectedIngredient 
        {
            get 
            {
                return selectedIngredient;
            }
            set 
            {
                selectedIngredient = value;
                OnPropertyChanged(nameof(SelectedIngredient));
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

            ApplicationContext.Instance.Medicines = new List<Entity>(Items);
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
            Items = new ObservableCollection<Entity>(repository.Search(Search, Sort));
        }

    }
}
