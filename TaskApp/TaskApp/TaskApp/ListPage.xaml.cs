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
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
            BindingContext = new ListPageViewModel();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }



    class ListPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tareas> Items { get; set; }
        public ObservableCollection<Grouping<string, Tareas>> ItemsGrouped { get; set; }

        public ListPageViewModel()
        {
            Items = new ObservableCollection<Tareas>();
            var sorted = from item in Items
                         orderby item.Name
                         group item by item.Name[0].ToString() into itemGroup
                         select new Grouping<string, Tareas>(itemGroup.Key, itemGroup);

            ItemsGrouped = new ObservableCollection<Grouping<string, Tareas>>(sorted);

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        }

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            Items = new ObservableCollection<Tareas>();
            IsBusy = true;
            var result = await XamarinAPI.Methods.GetAllTask();
            foreach(Tareas tr in result)
            {
                Items.Add(tr);
            }
            await Task.Delay(2000);
            var sorted = from item in Items
                         orderby item.Name
                         group item by item.Name[0].ToString() into itemGroup
                         select new Grouping<string, Tareas>(itemGroup.Key, itemGroup);

            ItemsGrouped = new ObservableCollection<Grouping<string, Tareas>>(sorted);

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
