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
        private List<Domain.Models.Element> Groups { get; set; } = 
            new List<Domain.Models.Element>();

        public Page1()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (App.Connection)
            {
                string page = Path.Combine(App.LocalFolderPath, "groups_page.html");
                string address = @"http://mgke.minsk.edu.by/ru/main.aspx?guid=3791";

                if (File.Exists(page)) File.Delete(page);

                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFile(address, page);
                }

                if (!File.Exists(page))
                {
                    await DisplayAlert("", "Не удалось получить данные с сайта МГКЭ!", "OK");
                }
                else
                {
                    await DisplayAlert("", "Страницы успешно загружены!", "OK");
                }
            }
            else
            {
                await DisplayAlert("", "Отсутствует подключение к сети Интернет!", "OK");
            }

            Parser parser = new Parser(Path.Combine(App.LocalFolderPath, "groups_page.html"));
            var list = parser.ParsePage(true);

            using (DataContext db = new DataContext())
            {
                await db.AddRangeAsync(list);
                await db.SaveChangesAsync();

                //this.FindByName<ListView>("listVIew_1").ItemsSource = db.Elements.Where(e => e.IsGroup == true);
            }


        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}