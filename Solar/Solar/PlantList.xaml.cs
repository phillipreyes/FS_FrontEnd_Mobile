using Newtonsoft.Json;
using Solar.Controller;
using Solar.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Solar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlantList : ContentPage
    {
        private TokenDTO userTokenInfo;
        private List<PlantInfo>plantList;
        public PlantList(TokenDTO userInfo)
        {   
            InitializeComponent();
            BindingContext = Application.Current;
            
            
            var plantNames = new List<PlantInfo>
            {
                new PlantInfo {PlantName = "First Plant", Id = "temp"},
                new PlantInfo {PlantName = "Second Plant", Id = "temp"},
                new PlantInfo {PlantName = "Third Plant", Id = "temp3"}
            };
            
            userTokenInfo = userInfo;
            Title = "Plant List";                    
            // var app = Application.Current as App;
            // plantNames.Add(new PlantInfo { PlantName = app.Username });            
        }
        protected override void OnAppearing()
        {
            GetPlantList getplantInfo = new GetPlantList(userTokenInfo);
            Debug.WriteLine("\n\nGETTING PLANTLIST\n\n");

            plantList = getplantInfo.getPLants();
            foreach (PlantInfo x in plantList)
            {
                Debug.WriteLine("ID : " + x.Id + "\nPlantName : " + x.PlantName);
            }
            Debug.WriteLine("Done");
            Plants.ItemsSource = plantList;
           
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
