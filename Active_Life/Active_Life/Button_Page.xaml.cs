using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;


namespace Active_Life
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class Button_Page : ContentPage
    {
        
        SensorSpeed speed = SensorSpeed.Default; //ustawienie opóźnienie prędkości dla zmian monitorowania
        public static int tryb_ciemny = Preferences.Get("trybciemny", 0); //pobranie wartosci podczas uruchamiania
        public static int licznik_krokow = Preferences.Get("licznikkrokow", 0);
        public double pomiar_wczesniejszy= 0;
        public static Color tlo_kompasu = Color.Blue;
        public static Color tlo_trasy = Color.White;
        public Button_Page()
        {
         
            InitializeComponent();
            DateTime dzien = DateTime.Now;
            data_godzina.Text = dzien.ToString("yyyy-MM-dd hh:mm");
            Tryb_Clicked(null, EventArgs.Empty);
            Accelerometer.Start(speed);
            Accelerometer.ReadingChanged += licz_kroki; //czytanie zmian

        }
        private async void licz_kroki(object sender, AccelerometerChangedEventArgs e)
        {
            
            float x_acceleration= e.Reading.Acceleration.X; //wczytywanie
            float y_acceleration= e.Reading.Acceleration.Y;
            float z_acceleration =e.Reading.Acceleration.Z;
            
            double pomiar=Math.Sqrt((x_acceleration * x_acceleration) + (y_acceleration * y_acceleration) + (z_acceleration * z_acceleration)); //algorytm pomiaru przesuniecia
            double roznica= pomiar - pomiar_wczesniejszy;//algorytm pomiaru przesuniecia
            pomiar_wczesniejszy = pomiar;
            if (roznica > 0.2 && roznica<0.4) //jezeli roznica przesuniecie miesci sie w ustalonym przedziale 
            {
                licznik_krokow+=1; //dodajemy krok
                Preferences.Set("licznikkrokow", licznik_krokow); //przechowywanie wartosci
            }

            
            liczba_krokow.Text = "Licznik kroków: " + licznik_krokow.ToString(); //wypisanie wartosci
      
        }
       
        async private void Trasa_Clicked(object sender, EventArgs e)
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
            
            
            if (tryb_ciemny==1) //wlaczenie trybu ciemnego
            {
            Preferences.Set("trybciemny", tryb_ciemny);
            trybciemny();
            button_tryb.Text = "tryb ciemny";
            } else if(tryb_ciemny==2) //wlaczenie trybu jasnego
            {
                Preferences.Set("trybciemny", tryb_ciemny);
                trybjasny();
                button_tryb.Text = "tryb jasny";

            } else if(tryb_ciemny==3) //wlaczenie trybu automatycznego
            {
                Preferences.Set("trybciemny", tryb_ciemny);
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
                tryb_ciemny =0;
                Preferences.Set("trybciemny", tryb_ciemny);
            }
            tryb_ciemny++;


        }
    }
}