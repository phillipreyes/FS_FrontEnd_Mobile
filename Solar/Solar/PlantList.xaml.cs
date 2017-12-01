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
    // Lists the plants that a user is authorized to view    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlantList : ContentPage
    {
        private TokenDTO userTokenInfo;
        private List<PlantInfo>plantList;
        public PlantList(TokenDTO userInfo)
        {   
            InitializeComponent();
            BindingContext = Application.Current;            
            
            // Page's title and token information
            userTokenInfo = userInfo;
            Title = "Plant List";                    
        }
        protected override void OnAppearing()
        {
            GetPlantList getplantInfo = new GetPlantList(userTokenInfo);
            // Debugging messages
            Debug.WriteLine("\n\nGETTING PLANTLIST\n\n");

            // List holding each Plant Information Object is created
            plantList = getplantInfo.getPlants();
            foreach (PlantInfo x in plantList)
            {
                Debug.WriteLine("ID : " + x.Id + "\nPlantName : " + x.PlantName);
            }
            Debug.WriteLine("Done");

            // Setup the page's display to use plantList
            Plants.ItemsSource = plantList;           
        }
        
        private async void Plants_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // if nothing selected don't run any coe
            if (Plants.SelectedItem == null)
                return;
            
            // Otherwise, pass the Selected Plant's Information to the Plant View XAML Page
            var plant = e.SelectedItem as PlantInfo;
            await Navigation.PushAsync(new PlantView(plant, userTokenInfo));
            // if go back to plant page ensures previously selected item is already selected
            Plants.SelectedItem = null;           
        }
        
    }
}
