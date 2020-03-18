using NeverSkipLegDay.Models.DAL;
using NeverSkipLegDay.ViewModels;
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
        public SetsPageViewModel ViewModel
        {
            get { return BindingContext as SetsPageViewModel; }
            set { BindingContext = value; }
        }
        public SetsPage(ExerciseViewModel exercise)
        {
            var setDal = new SetDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new SetsPageViewModel(exercise, setDal, pageService);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var exercise = new ExerciseViewModel(new ExerciseDal(new SQLiteDB()).GetExercise(ViewModel.Exercise.Id));
            var setDal = new SetDal(new SQLiteDB());
            var pageService = new PageService();
            ViewModel = new SetsPageViewModel(exercise, setDal, pageService);
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
        }
    }
}