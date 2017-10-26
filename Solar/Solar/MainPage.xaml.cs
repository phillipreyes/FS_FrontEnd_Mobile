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
        HttpClient client = new HttpClient();
        List<Model.PlantInfo> PlantItems = new List<Model.PlantInfo>();
        PlantListClass list = new PlantListClass();
        public MainPage()
        {
            InitializeComponent();

            BindingContext = Application.Current;
        }

        
        private async void Login_Clicked(object sender, EventArgs e)
        {
            /* UserClass user = new UserClass();
             Login login ;
             Debug.WriteLine("hey");
             if (UserName.Text == "test" && Password.Text == "pass")
             {
                 user.Email = UserName.Text;
                 user.password = Password.Text;
                 login = new Login(user);
                 var plantList = login.TryLogin();
             }
             if (UserName.Text == "user" && Password.Text == "pass")
            // if (Password.Text == "P@ss0rd" && )
             {
                 //move to next page

                 user.Email = UserName.Text;
                 user.password = Password.Text;
                 login = new Login(user);
                 var plantList = login.TryLogin();


                 Warning.Text = "";
                 Warning.TextColor = Color.Black;
                 var app = Application.Current as App;
                 app.Username = UserName.Text;
                 await Application.Current.SavePropertiesAsync();
                 await Navigation.PushAsync(new PlantList());
             }
             else
             {
                 Warning.Text = "Incorrect username or password";
                 Warning.TextColor = Color.Red;
             }*/

            //if (UserName.Text == "user" && Password.Text == "pass")
            //const string Url = "https://jsonplaceholder.typicode.com/posts";
            //HttpClient _client = new HttpClient();

            if (Password.Text == "pass")
            {
                Warning.Text = "";
                Warning.TextColor = Color.Black;

                User user = new User();
                user.email = "testuser1@fsweb.com";
                user.password = "P@ssw0rd";
                user.grant_type = "password";

                Login log = new Login(user);

                List<PlantInfo> plantlist = new List<PlantInfo>();
                plantlist = log.TryLogin();
                var app = Application.Current as App;
                app.PlantListKey = plantlist;

                app.Username = UserName.Text;
                await Application.Current.SavePropertiesAsync();
               // PlantList plant = new PlantList();
                await Navigation.PushAsync(new PlantList());
            }
            else
            {
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
