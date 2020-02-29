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
    public partial class SetsPage : ContentPage
    {
        static int render = 0;
        public SetsPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Models.Exercise exercise = (Models.Exercise)this.BindingContext;
            render = 0;
            listView.ItemsSource = await App.SetDAL.GetSetsByExerciseIdAsync(exercise.ID);
        }

        async void OnSave(object sender, EventArgs e)
        {
            Button save = (Button)sender;
            Models.Set set = (Models.Set)save.BindingContext;
            set.Date = DateTime.UtcNow;
            await App.SetDAL.SaveSetAsync(set);
            save.IsEnabled = false;
            save.Opacity = 0;
        }

        private void CancelSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ((ListView)sender).SelectedItem = null;
        }

        void OnEdit(object sender, EventArgs e)
        {
            Models.Exercise exercise = (Models.Exercise)BindingContext;
            Entry entry = (Entry)sender;
            StackLayout stackLayout = (StackLayout)entry.Parent;
            Button save = (Button)stackLayout.Children.Last();

            if (render > ((exercise.Sets * 2))-1)
            {
                save.IsEnabled = true;
                save.Opacity = 1;
            }

            render++;
        }
    }
}