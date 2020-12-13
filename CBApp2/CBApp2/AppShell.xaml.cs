using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp2.Domain.Services;

namespace CBApp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
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