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
            if (Active_Life.Button_Page.tryb_ciemny == 1)
            {
                Trasa_Page.BackgroundColor = Color.Black;
                Trasa_Page.BackgroundColor = Color.Black;
                napis.BackgroundColor = Color.White;
            }
        }
    }
}