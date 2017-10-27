using Solar.Controller;
using Solar.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;
using Xamarin.Forms;

namespace Solar
{
    public partial class MainPage : ContentPage
    {
        // HttpClient client = new HttpClient();
        // List<Model.PlantInfo> PlantItems = new List<Model.PlantInfo>();
        // PlantListClass list = new PlantListClass();
        private bool isAuthen = false;
      
        public MainPage()
        {
            InitializeComponent();

            BindingContext = Application.Current;
        }

        
        private async void Login_Clicked(object sender, EventArgs e)
        {

            User user = new User();
            /*user.email = "testuser1@fsweb.com";
            user.password = "P@ssw0rd";
            user.grant_type = "password";
            */
            user.email = UserName.Text;
            user.password = Password.Text;
            user.grant_type = "password";
            Login log = new Login(user);
            Login.IsEnabled = false;
            // List<PlantInfo> plantlist = new List<PlantInfo>();
           
              //login n  
            isAuthen =  await log.TryLogin();
            Debug.WriteLine("returned = " + isAuthen);
            if (isAuthen)
            {
                Login.IsEnabled = true;      
                Warning.Text = "";
                Warning.TextColor = Color.Black;
                var app = Application.Current as App;
               //  app.PlantListKey = plantlist;

                app.Username = UserName.Text;
                await Application.Current.SavePropertiesAsync();
                // PlantList plant = new PlantList();
                await Navigation.PushAsync(new PlantList());
             
            }
            else
            {
                Login.IsEnabled = true;
                Warning.Text = "Incorrect username or password";
                Warning.TextColor = Color.Red;
            }




        }

        private void Forgot_Clicked(object sender, EventArgs e)
        {
            Warning.Text = "Not yet implemented";
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RequestAccount());
        }

        private void UserName_Completed(object sender, EventArgs e)
        {
            Password.Focus();
        }
    }
}
