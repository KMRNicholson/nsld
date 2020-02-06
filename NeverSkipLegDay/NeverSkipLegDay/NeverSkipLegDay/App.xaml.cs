using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NeverSkipLegDay.DAL;

namespace NeverSkipLegDay
{
    public partial class App : Application
    {
        static WorkoutDAL workoutDAL;

        public static WorkoutDAL WorkoutDAL
        {
            get
            {
                if(workoutDAL == null)
                {
                    workoutDAL = new WorkoutDAL(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workouts.db3"));
                }
                return workoutDAL;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
