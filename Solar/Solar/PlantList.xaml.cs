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
                new PlantInfo {PlantName = "First Plant", Id = "temp"},
                new PlantInfo {PlantName = "Second Plant", Id = "temp"},
                new PlantInfo {PlantName = "Third Plant", Id = "temp3"}
            };

            var app = Application.Current as App;
            plantNames.Add(new PlantInfo { PlantName = app.Username });

            Plants.ItemsSource = plantNames;
        }

        private async void Plants_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (Plants.SelectedItem == null)
                return;
            var app = Application.Current as App;
            var plant = e.SelectedItem as PlantInfo;
            app.CurrentPlant = plant.PlantName;
            await Navigation.PushAsync(new PlantView(plant));
            Plants.SelectedItem = null;
        }
    }
}
