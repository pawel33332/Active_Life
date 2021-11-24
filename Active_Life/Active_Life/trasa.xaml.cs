using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Active_Life
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Trasa : ContentPage
    {
        public Trasa()
        {
            InitializeComponent();
            Trasa_Page.BackgroundColor = Active_Life.Button_Page.tlo_trasy;
        }
    }
}