using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeverSkipLegDay.Workouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseProgress : ContentPage
    {
        public ExerciseProgress()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Models.Exercise exercise = (Models.Exercise)this.BindingContext;
            listView.ItemsSource = await App.SetDAL.GetSetsByExerciseIdAsync(exercise.ID);
        }

        async void OnSave(object sender, EventArgs e)
        {
            var set = (Models.Set)BindingContext;
            set.Date = DateTime.UtcNow;
            await App.SetDAL.SaveSetAsync(set);
        }

        private void CancelSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ((ListView)sender).SelectedItem = null;
        }
    }
}