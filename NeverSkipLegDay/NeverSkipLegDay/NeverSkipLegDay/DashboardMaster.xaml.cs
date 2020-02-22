using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeverSkipLegDay
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardMaster : ContentPage
    {
        public ListView ListView;

        public DashboardMaster()
        {
            InitializeComponent();

            BindingContext = new DashboardMasterViewModel();
            ListView = MenuItemsListView;
        }

        class DashboardMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<DashboardMasterMenuItem> MenuItems { get; set; }

            public DashboardMasterViewModel()
            {
                MenuItems = new ObservableCollection<DashboardMasterMenuItem>(new[]
                {
                    new DashboardMasterMenuItem { Id = 0, Title = "Workouts" },
                    new DashboardMasterMenuItem { Id = 1, Title = "Nutrition" },
                    new DashboardMasterMenuItem { Id = 2, Title = "Records" },
                    new DashboardMasterMenuItem { Id = 3, Title = "Account" },
                    new DashboardMasterMenuItem { Id = 4, Title = "Settings" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DashboardMasterMenuItem item = (DashboardMasterMenuItem)e.SelectedItem;
            if (item != null)
            {
                switch (item.Title)
                {
                    case "Workouts":
                        ((ListView)sender).SelectedItem = null;
                        await Navigation.PushAsync(new Workouts.Workouts());
                        break;
                    case "Nutrition":
                        break;
                    case "Record":
                        break;
                    case "Account":
                        break;
                    case "Settings":
                        break;
                    default:
                        break;
                }
            }
        }
    }
}