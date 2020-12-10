using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBApp2
{
    public partial class App : Application
    {
        public static bool Connection
        {
            get
            {
                return DependencyService.Get<Services.IConnectionInfo>().IsConnected;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
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
