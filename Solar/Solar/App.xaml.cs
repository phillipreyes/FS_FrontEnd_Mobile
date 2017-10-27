using Solar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Solar
{
    public partial class App : Application
    {
        private const string UsernameKey = "user";
        private const string PlantKey = "currentPlantKey";

        public string usercross = "";
        //public List<PlantInfo> PlantListKey = new List<PlantInfo>();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new MainPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public string Username
        {
            get
            {
                if (Properties.ContainsKey(UsernameKey))
                    return Properties[UsernameKey].ToString();

                return "";
            }

            set
            {
                Properties[UsernameKey] = value;
            }
        }

        public string CurrentPlant
        {
            get
            {
                if (Properties.ContainsKey(PlantKey))
                    return Properties[PlantKey].ToString();
                return "";
            }

            set
            {
                Properties[PlantKey] = value;
            }
        }
       
        /* public List<PlantInfo> PlantList
        {
            get
            {
                if (Properties.ContainsKey(PlantListKey.ToString()))
                    return Properties[PlantListKey];
                return null;
            }

        }*/
    }
}
