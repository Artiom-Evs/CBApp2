using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp2.Infrastructure.Services;
using CBApp2.Domain.Services;
using CBApp2.Domain.Models;

namespace CBApp2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DaysViewPage1 : ContentPage
    {
        CBApp2.Domain.Models.Element Element { get; set; }
        List<Day> Items { get; set; } = new List<Day>();

        public DaysViewPage1(CBApp2.Domain.Models.Element element)
        {
            InitializeComponent();

            this.Element = element;
            this.FindByName<Label>("frame_1").BindingContext = element;
            
            using (DataContext db = new DataContext())
            {
                var week = db.Weeks.First(w => w.ElementId == Element.Id);
                Items = db.Days.Where(d => d.WeekId == week.Id).OrderBy(d => d.Number).ToList();
            }

            this.FindByName<ListView>("MyListView").ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new Pages.DayViewPage(e.Item as Day));
        }
    }
}
