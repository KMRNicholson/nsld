using System;
using System.IO;
using Xamarin.Forms;
using NeverSkipLegDay.DAL;

namespace NeverSkipLegDay
{
    public partial class App : Application
    {
        static WorkoutDAL workoutDAL;
        static ExerciseDAL exerciseDAL;
        static SetDAL setDAL;
        public static WorkoutDAL WorkoutDAL
        {
            get
            {
                if(workoutDAL == null)
                {
                    workoutDAL = new WorkoutDAL(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Views.db3"));
                }
                return workoutDAL;
            }
        }

        public static ExerciseDAL ExerciseDAL
        {
            get
            {
                if (exerciseDAL == null)
                {
                    exerciseDAL = new ExerciseDAL(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Exercises.db3"));
                }
                return exerciseDAL;
            }
        }
        public static SetDAL SetDAL
        {
            get
            {
                if (setDAL == null)
                {
                    setDAL = new SetDAL(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sets.db3"));
                }
                return setDAL;
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
