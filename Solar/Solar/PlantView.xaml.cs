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
        public PlantView(PlantInfo info, TokenDTO td)
        {
            InitializeComponent();

            // BindingContext = info;
            GenCalender gc = new GenCalender(info, td);
            // GenCalender gc = new GenCalender(4141, );
            DateTime day1 = new DateTime(2017, 9, 1);
            DateTime day2 = new DateTime(2017, 9, 1);
            // gc.GenerateDataModel(day1, day2);
            BindingContext = gc;

            // var app = Application.Current as App;
            // ContentPage.TitleProperty = app.CurrentPlant;
            // SelectedPlant.Text = info.PlantName;            
        }
    }
}
