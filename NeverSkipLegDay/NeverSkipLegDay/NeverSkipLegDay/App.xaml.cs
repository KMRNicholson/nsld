using System;
using System.IO;
using Xamarin.Forms;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay
{
    public partial class App : Application
    {
        static readonly SQLiteDB Db = new SQLiteDB();
        static WorkoutDal workoutDal;
        static ExerciseDal exerciseDal;
        static SetDal setDal;
        public static WorkoutDal WorkoutDal
        {
            get
            {
                if(workoutDal == null)
                {
                    workoutDal = new WorkoutDal(Db);
                }
                return workoutDal;
            }
        }

        public static ExerciseDal ExerciseDal
        {
            get
            {
                if (exerciseDal == null)
                {
                    exerciseDal = new ExerciseDal(Db);
                }
                return exerciseDal;
            }
        }
        public static SetDal SetDal
        {
            get
            {
                if (setDal == null)
                {
                    setDal = new SetDal(Db);
                }
                return setDal;
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
