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
    public partial class PlantView : ContentPage
    {

        public PlantView(PlantInfo info)
        {
            InitializeComponent();

            BindingContext = info;

            var app = Application.Current as App;
            //ContentPage.TitleProperty = app.CurrentPlant;
            SelectedPlant.Text = info.Id;
        }
    }
}
