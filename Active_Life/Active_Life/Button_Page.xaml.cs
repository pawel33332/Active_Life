using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Active_Life
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class Button_Page : ContentPage
    {
        
        public static int tryb_ciemny = 0;
        public static Color tlo_kompasu = Color.Blue;
        public static Color tlo_trasy = Color.White;
        public Button_Page()
        {
         
            InitializeComponent();
            DateTime dzien = DateTime.Now;
            data_godzina.Text = dzien.ToString("yyyy-MM-dd hh:mm");
        }
       async  private void Trasa_Clicked(object sender, EventArgs e)
        {
   
        await Navigation.PushAsync(new Trasa());
       

        }
        async private void Kompas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Kompas());
        }
        void trybjasny()
        {
            button_trasa.Source = "button_trasa.png";
            button_kompas.Source = "button_kompas.png";
            Button_Pagee.BackgroundColor = Color.White;
            button_tryb.BackgroundColor = Color.Gray;
            data_godzina.TextColor = Color.Black;
            liczba_krokow.TextColor = Color.Black;
            tlo_kompasu = Color.Blue;
            tlo_trasy = Color.White;
        }
        void trybciemny()
        {
            button_trasa.Source = "button_trasa_ciemny.png";
            button_kompas.Source = "button_kompas_ciemny.png";
            data_godzina.TextColor = Color.White;
            liczba_krokow.TextColor = Color.White;
            Button_Pagee.BackgroundColor = Color.Black;
            button_tryb.BackgroundColor = Color.Gray;
            tlo_kompasu = Color.Black;
            tlo_trasy = Color.Black;
        }
        void Tryb_Clicked(object sender, EventArgs e)
        {
            tryb_ciemny++;
            if(tryb_ciemny==1) //wlaczenie trybu ciemnego
            {
            trybciemny();
            button_tryb.Text = "tryb ciemny";
            } else if(tryb_ciemny==2) //wlaczenie trybu jasnego
            {
                trybjasny();
                button_tryb.Text = "tryb jasny";

            } else if(tryb_ciemny==3) //wlaczenie trybu automatycznego
            {
                button_tryb.BackgroundColor = Color.Gray;
                button_tryb.Text = "auto tryb";
                DateTime dzien = DateTime.Now;
                int miesiac = dzien.Month;
                int godzina = dzien.Hour;
                
                switch (miesiac)
                {
                    case 1:
                        if (godzina > 8 && godzina < 16)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 2:
                        if (godzina > 7 && godzina < 16)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 3:
                        if (godzina > 6 && godzina < 18)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 4:
                        if (godzina > 6 && godzina < 19)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 5:
                        if (godzina > 5 && godzina < 20)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 6:
                        if (godzina > 5 && godzina < 21)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 7:
                        if (godzina > 5 && godzina < 20)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 8:
                        if (godzina > 6 && godzina < 19)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 9:
                        if (godzina > 7 && godzina < 18)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 10:
                        if (godzina > 7 && godzina < 17)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                    case 11:
                        if (godzina > 8 && godzina < 16)
                        {
                            trybjasny();
                        } else
                        {
                            trybciemny();
                        }
                        break;
                    case 12:
                        if (godzina > 8 && godzina < 16)
                        {
                            trybjasny();
                        }
                        else
                        {
                            trybciemny();
                        }
                        break;
                }

            } else
            {
                tryb_ciemny = 0;
            }
          

            
        }
    }
}