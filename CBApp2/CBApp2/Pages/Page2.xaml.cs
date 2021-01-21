using System;
using System.IO;
using System.Collections.Generic;
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
    public partial class Page2 : ContentPage
    {
        private List<Domain.Models.Element> Items { get; set; } =
            new List<Domain.Models.Element>();

        public Page2()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!App.LoadTeachers.Result)
            {
                await DisplayAlert("", "Не удалось получить данные с сайта МГКЭ!\nБудет использована последняя загруженная версия.", "OK");
            }

            using (DataContext db = new DataContext())
            {
                Items = db.Elements.Where(e => e.IsGroup == false).OrderBy(e => e.Name).ToList();
            }

            this.FindByName<ListView>("MyListView").ItemsSource = Items;
        }

        private async void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new Pages.DaysViewPage1(e.Item as CBApp2.Domain.Models.Element));
        }
    }
}

