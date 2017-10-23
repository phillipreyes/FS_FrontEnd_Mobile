using Solar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Solar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlantList : ContentPage
    {
        public PlantList()
        {
            InitializeComponent();
            
            var plantNames = new List<PlantInfo>
            {
                new PlantInfo {name = "First Plant", id = "temp"},
                new PlantInfo {name = "Second Plant", id = "temp"},
                new PlantInfo {name = "Third Plant"}
            };

            var app = Application.Current as App;
            plantNames.Add(new PlantInfo { name = app.Username });

            Plants.ItemsSource = plantNames;
        }

        private async void Plants_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (Plants.SelectedItem == null)
                return;
            var app = Application.Current as App;
            var plant = e.SelectedItem as PlantInfo;
            app.CurrentPlant = plant.name;
            await Navigation.PushAsync(new PlantView(plant));
            Plants.SelectedItem = null;
        }
    }
}
