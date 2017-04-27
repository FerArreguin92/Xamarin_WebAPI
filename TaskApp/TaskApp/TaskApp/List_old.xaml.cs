using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskApp.Models;
using TaskApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List_old : ContentPage
    {
        public List_old()
        {
            InitializeComponent();
            BindingContext = new List_oldViewModel();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            Item it = e.SelectedItem as Item;
            //await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");
            Tareas itemTarea = await XamarinAPI.Methods.GetTaskById(int.Parse(it.Text));
            await DisplayAlert("Name - Description", itemTarea.Name + " " + itemTarea.Description, "OK");
            await DisplayAlert("End Date", itemTarea.EndDate.ToString("dd/MM/yyyy HH:mm"), "OK");
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }



    class List_oldViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; }

        public List_oldViewModel()
        {
            Items = new ObservableCollection<Item>();
            foreach (Tareas tr in App.LstTareas)
            {
                Items.Add(new Item { Text = tr.Id.ToString(), Detail = tr.Name + " - " + tr.Description });
            }

            var sorted = from item in Items
                         orderby item.Text
                         group item by item.Text[0].ToString() into itemGroup
                         select new Grouping<string, Item>(itemGroup.Key, itemGroup);

            ItemsGrouped = new ObservableCollection<Grouping<string, Item>>(sorted);

            //RefreshDataCommand = new Command(
            //    async () => await RefreshData());
        }

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
            //Load Data Here
            await Task.Delay(2000);

            IsBusy = false;
        }

        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        

        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Items.Add(item);
            }
        }
    }
}
public class Item
{
    public string Text { get; set; }
    public string Detail { get; set; }

    public override string ToString() => Text;
}