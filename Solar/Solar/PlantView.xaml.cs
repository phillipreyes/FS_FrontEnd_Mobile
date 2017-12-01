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
            // Pass Plant information selected from Plant List Page to the GenCalendar object
            // so that the generation calendar object is working with the correct plant
            // creating this object automatically generates the generation calendar
            // currently it always generates for September 1st 2017
            GenCalender gc = new GenCalender(info, td);
            // Setup the Generaton Calendar to be used by the XAML Plant View Page
            BindingContext = gc;
        }
    }
}
