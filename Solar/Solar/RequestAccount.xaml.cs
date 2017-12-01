using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Solar
{
    // Unimplemented Placeholder class
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestAccount : ContentPage
    {
        public RequestAccount()
        {
            InitializeComponent();

            BindingContext = Application.Current;
        }
    }
}