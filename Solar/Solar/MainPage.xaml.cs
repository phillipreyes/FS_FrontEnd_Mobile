using Solar.Controller;
using Solar.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Solar
{
    // Page to log into the application
    public partial class MainPage : ContentPage
    {      
        public MainPage()
        {
            InitializeComponent();

            BindingContext = Application.Current;
        }

        // Login button that attempts logging in when pressed
        private async void Login_Clicked(object sender, EventArgs e)
        {
            Tuple<TokenDTO, bool> tokenobj ;
            User user = new User();
            // auto-fill working login for quicker testing            
            // user.email = "testuser1@fsweb.com";
            // user.email = "testuser1@fsdev.com";
            // user.password = "P@ssw0rd";
            // user.grant_type = "password";
            
            user.email = UserName.Text;
            user.password = Password.Text;
            user.grant_type = "password";
            // Create the Login object
            Login log = new Login(user);
            Login.IsEnabled = false;            
           
            // Use TryLogin method of our Login object
            // actual logging in
            tokenobj =  await log.TryLogin();
            Debug.WriteLine("returned = " + tokenobj.Item2);
            // If login returns successful
            if (tokenobj.Item2)
            {
                Login.IsEnabled = true;      
                Warning.Text = "";
                Warning.TextColor = Color.Black;

                await Application.Current.SavePropertiesAsync();
                // redirect to the Plant List Page
                // giving it the authorization token it recieved from logging in
                // authorization token needed in all future data requests from the Web API
                await Navigation.PushAsync(new PlantList(tokenobj.Item1));
            }
            else
            {
                // failed login prompted
                Login.IsEnabled = true;
                Warning.Text = "Incorrect username or password";
                Warning.TextColor = Color.Red;
            }
        }

        // Unimplemented
        private void Forgot_Clicked(object sender, EventArgs e)
        {
            Warning.Text = "Not yet implemented";
        }

        // Unimplemented
        private async void Create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RequestAccount());
        }

        // When checkmark hit the cursor is automatically moved to password field
        private void UserName_Completed(object sender, EventArgs e)
        {
            Password.Focus();
        }
    }
}
