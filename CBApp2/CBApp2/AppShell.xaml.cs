﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBApp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Меню", "Здесь должно быть меню", "ОК");
        }

        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            App.Current.Quit();
        }
    }
}