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
        public static int tryb_ciemny = 0;
        public Button_Page()
        {
            InitializeComponent();
            DateTime dzien = DateTime.Now;
            data_godzina.Text = dzien.ToString();
        }
       async  private void Trasa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Trasa());
        }
        async private void Kompas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Kompas());
        }
        void Tryb_Clicked(object sender, EventArgs e)
        {
            tryb_ciemny++;
            if (tryb_ciemny == 1)
            {
                button_trasa.Source = "button_trasa_ciemny.png";
                button_kompas.Source = "button_kompas_ciemny.png";
                data_godzina.TextColor = Color.White;
                liczba_krokow.TextColor = Color.White;
                Button_Pagee.BackgroundColor = Color.Black;
                button_tryb.BackgroundColor = Color.Gray;
                button_tryb.Text = "Wyłącz tryb ciemny";
            }
            else
            {
                tryb_ciemny = 0;
                button_trasa.Source = "button_trasa.png";
                button_kompas.Source = "button_kompas.png";
                Button_Pagee.BackgroundColor = Color.White;
                button_tryb.BackgroundColor = Color.Black;
                button_tryb.Text = "Włącz tryb ciemny";
                data_godzina.TextColor = Color.Black;
                liczba_krokow.TextColor = Color.Black;

            }
        }
}