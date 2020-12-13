using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBApp2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (App.Connection)
            {
                string page = Path.Combine(App.LocalFolderPath, "teachers_page.html");
                string address = @"http://mgke.minsk.edu.by/ru/main.aspx?guid=3811";

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
        }
    }
}

