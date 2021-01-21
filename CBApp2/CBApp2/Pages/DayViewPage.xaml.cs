using System;
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
    public partial class DayViewPage : ContentPage
    {
        private Day selectedItem { get; set; }
        private List<Lesson> lessons { get; set; }

        public DayViewPage(Day day)
        {
            InitializeComponent();
            selectedItem = day;
            this.FindByName<Label>("frame_1").BindingContext = selectedItem;
            
            using (DataContext db = new DataContext())
            {
                lessons = db.Lessons.Where(l => l.DayId == selectedItem.Id).OrderBy(l => l.Number).ToList();
            }

            this.SelectContentGrid();
        }
        private void SelectContentGrid()
        {
            Grid grid = this.FindByName<Grid>("grid_1");

            for (int i = 0; i < lessons.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });
            }

            var p = Padding;
            p.Bottom = 10;

            for (int i = 0; i < lessons.Count; i++)
            {
                grid.Children.Add(new Label
                    {
                        Text = (i + 1).ToString(),
                        HorizontalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 24
                    }, 0, i);
                grid.Children.Add(new Label
                {
                    Text = (lessons[i].Subject.Replace(") ", ")\n") + "\n" + lessons[i].Room)
                        .TrimEnd("\n".ToCharArray()),
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Padding = p,
                    FontSize = 24
                    
                    //.Replace("2.", "\n2.").Replace("3.", "\n3.");
                    //if ("\n" == text) text = "-";
            }, 1, i);
            }

            this.UpdateChildrenLayout();
        }
    }
}