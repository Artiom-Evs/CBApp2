﻿using System;
using System.IO;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBApp2
{
    public partial class App : Application
    {
        public static string LocalFolderPath { get; } =
            Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string DatabasePath { get; } = Path.Combine(LocalFolderPath, "database.db");
        public static Task<bool> LoadGroups, LoadTeachers;

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

            //File.Delete(DatabasePath);

            MainPage = new AppShell();
            //MainPage = new NavigationPage(new Pages.LoadingPage());
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
