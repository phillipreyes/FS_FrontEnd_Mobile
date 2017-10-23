using Solar.Controller;
using Solar.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SqlClient;
using Xamarin.Forms;

namespace Solar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = Application.Current;
        }

        
        private async void Login_Clicked(object sender, EventArgs e)
        {
            UserClass user = new UserClass();
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
