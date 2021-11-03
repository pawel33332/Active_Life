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
    public partial class Button_Page : ContentPage
    {
        public Button_Page()
        {
            InitializeComponent();       
        }

       async  private void Trasa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Trasa());
        }
        async private void Kompas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Kompas());
        }
        async private void Tryb_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Tryb());
        }
    }
}