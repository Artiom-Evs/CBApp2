using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace CBApp2.Domain.Services
{
    public static class Downloader
    {
        public static async Task<bool> LoadSitePageAsync()
        {
            return await LoadSitePageAsync("page.html");
        }
        public static async Task<bool> LoadSitePageAsync(string path)
        {
            return await LoadSitePageAsync(
                "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791",
                path);
        }
        public static async Task<bool> LoadSitePageAsync(string address, string path)
        {
            WebClient client = new WebClient();

            await client.DownloadFileTaskAsync(
                new System.Uri(address), path);

            return await Task.FromResult(File.Exists(path));
        }

        public static void LoadSitePage()
        {
            LoadSitePage("page.html");
        }
        public static void LoadSitePage(string path)
        {
            //преподаватели "http://mgke.minsk.edu.by/ru/main.aspx?guid=3811"
            //учащиеся "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791"
            LoadSitePage(
                "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791",
                path);
        }
        public static void LoadSitePage(string address, string path)
        {
            WebClient client = new WebClient();

            client.DownloadFile(
                new System.Uri(address), path);
        }
    }
}




