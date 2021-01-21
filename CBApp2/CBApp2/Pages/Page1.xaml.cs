using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp2.Infrastructure.Services;
using CBApp2.Domain.Services;
using CBApp2.Domain.Models;

namespace CBApp2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private List<Domain.Models.Element> Items { get; set; } = 
            new List<Domain.Models.Element>();

        ActivityIndicator activityIndicator = new ActivityIndicator();

        public Page1()
        {
            //this.FindByName<StackLayout>("mainStack").Children.Add(activityIndicator);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!App.LoadGroups.Result)
            {
                await DisplayAlert("", "Не удалось получить данные с сайта МГКЭ!\nБудет использована последняя загруженная версия.", "OK");
            }

            using (DataContext db = new DataContext())
            {
                Items = db.Elements.Where(e => e.IsGroup == true).ToList();
            }

            this.FindByName<ListView>("MyListView").ItemsSource = Items;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new Pages.DaysViewPage1(e.Item as CBApp2.Domain.Models.Element));
        }
    }
}