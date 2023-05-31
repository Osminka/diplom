using diplom.Services;
using diplom.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using App1.Views;

namespace diplom
{
    public partial class App : Application
    {
        private static DB db;
        public static DB Db
        {
            get
            {
                if (db == null)
                    db = new DB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.mobileosm"));
                return db;
            }
        }
        public static bool AdmOrNo = false; 
        public App()
        {
            InitializeComponent();
            AdmOrNo = false;
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockDataStoreRules>();
            DependencyService.Register<MockDataStoreLit>();
            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            MainPage = new RegistrPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
