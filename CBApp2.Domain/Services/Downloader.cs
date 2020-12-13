using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace CBApp2.Domain.Services
{
    public static class Downloader
    {
        //преподаватели "http://mgke.minsk.edu.by/ru/main.aspx?guid=3811"
        //учащиеся "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791"

        public static async Task<bool> LoadSitePageAsync(string path, string address)
        {
            WebClient client = new WebClient();

            await client.DownloadFileTaskAsync(
                new System.Uri(address), path);

            return await Task.FromResult(File.Exists(path));
        }
        public static bool LoadSitePage(string address, string path)
        {
            WebClient client = new WebClient();

            client.DownloadFile(
                new System.Uri(address), path);

            return File.Exists(path);
        }
        public static void GetSitePage(string address, string filename)
        {
            WebClient client = new WebClient();
            client.DownloadFile(new Uri(address), filename);
        }
    }
}




