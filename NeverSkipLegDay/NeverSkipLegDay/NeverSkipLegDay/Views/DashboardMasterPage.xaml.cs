using NeverSkipLegDay.Models;
using NeverSkipLegDay.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardMasterPage : ContentPage
    {
        public ListView ListView;

        public DashboardMasterPage()
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
                        await Navigation.PushAsync(new WorkoutsPage());
                        break;
                    case "Nutrition":
                        ((ListView)sender).SelectedItem = null;
                        await Navigation.PushAsync(new MealsPage());
                        break;
                    case "Records":
                        ((ListView)sender).SelectedItem = null;
                        await Navigation.PushAsync(new RecordExercisesPage(new WorkoutViewModel(new Workout())));
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