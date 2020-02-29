using Xamarin.Forms;
using NeverSkipLegDay.Models.DAL;

namespace NeverSkipLegDay
{
    public partial class App : Application
    {
        static readonly SQLiteDB Db = new SQLiteDB();
        static SetDal setDal;
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
