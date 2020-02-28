using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardDetail : ContentPage
    {
        public DashboardDetail()
        {
            InitializeComponent();
        }

        async void OnWorkoutsSelected(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Workouts());
        }
    }
}