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

namespace CBApp2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            App.LoadGroups = Task.Run(() => LoadGroups());
            App.LoadTeachers = Task.Run(() => LoadTeachers());
            ;

            InitializeComponent();
        }
        private async Task<bool> LoadGroups()
        {
            if (App.Connection)
            {
                string page = Path.Combine(App.LocalFolderPath, "groups_page.html");
                string address = @"http://mgke.minsk.edu.by/ru/main.aspx?guid=3791";
                
                if (File.Exists(page)) File.Delete(page);
                
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFileTaskAsync(new System.Uri(address), page).Wait();
                }

                if (File.Exists(page))
                {
                    this.AddToDatabase(page, true).Wait();
                    return true;
                }
            }
            else
            {
                await DisplayAlert("", "Отсутствует подключение к сети Интернет!", "OK");
            }

            return false;
        }
        private async Task<bool> LoadTeachers()
        {
            if (App.Connection)
            {
                string page = Path.Combine(App.LocalFolderPath, "teachers_page.html");
                string address = @"http://mgke.minsk.edu.by/ru/main.aspx?guid=3811";

                if (File.Exists(page)) File.Delete(page);

                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFileTaskAsync(new System.Uri(address), page).Wait();
                }

                if (File.Exists(page))
                {
                    this.AddToDatabase(page, false).Wait();
                    return true;
                }
            }
            else
            {
                await DisplayAlert("", "Отсутствует подключение к сети Интернет!", "OK");
            }

            return false;
        }
        private async Task AddToDatabase(string page, bool isGroup)
        {
            Parser parser = new Parser();
            var list = Task.Run(() => parser.ParsePage(page, isGroup));

            using (DataContext db = new DataContext())
            {
                foreach (var elem in db.Elements.Where(e => e.IsGroup == isGroup))
                {
                    db.Elements.Remove(elem);
                }

                await db.AddRangeAsync(list.Result);
                await db.SaveChangesAsync();
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Обновить данные", "Функция обновления данных пока не доступна", "ОК");
        }
        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            App.Current.Quit();
        }
    }
}
