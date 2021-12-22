using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Xamarin.Essentials;
using System.IO;

namespace Active_Life
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statystyki_Page : ContentPage
    {
        string trasa_file_stat = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path.txt");
        string wartosc;
        double[] tab_pozycja = new double[1000];
        double dlugosc_trasy=0;


        public Statystyki_Page()
        {
            InitializeComponent();
            Stat_Page.BackgroundColor = Active_Life.Button_Page.tlo_trasy;
            inf.TextColor = Active_Life.Button_Page.text_color;
            przebyta_droga.TextColor = Active_Life.Button_Page.text_color;
            srednia_predkosc.TextColor = Active_Life.Button_Page.text_color;
            czas_trasy.TextColor = Active_Life.Button_Page.text_color;
            data.TextColor = Active_Life.Button_Page.text_color;
            if (File.Exists(trasa_file_stat))
            {
                inf.Text = "Statystyki twojej ostatniej trasy";
                odczytaj_trase();
            } else
            {
                inf.Text = "Nie masz żadnej zapisanej trasy";
            }
            
        }
        private void odczytaj_trase()
        {
            Polyline polyline = new Polyline
            {
                StrokeWidth = 20,
                StrokeColor = Color.FromHex("#dd9ecd"),

                Geopath =
            {


            }
            };
            using (StreamReader sr = new StreamReader(trasa_file_stat))
            {
                int i = 0;
                while ((wartosc = sr.ReadLine()) != null)
                {
                    if (i % 2 == 0)
                    {

                        float lattitude = float.Parse(wartosc);

                        tab_pozycja[i] = lattitude;

                    }
                    else
                    {
                        float longitude = float.Parse(wartosc);
                        tab_pozycja[i] = longitude;
                    }
                    i++;
                } 
                for (int a = 0; a < i-1; a += 2)
                {
                   
                    polyline.Geopath.Add(new Position(tab_pozycja[a], tab_pozycja[a + 1]));

                }
                for(int a=0;a<=i-3;a+=4)
                {
                    var a_lattitude = tab_pozycja[a];
                    var a_longtitude = tab_pozycja[a + 1];
                    var b_lattitude = tab_pozycja[a + 2];
                    var b_longtitude = tab_pozycja[a + 3];
                    Location pierwsza = new Location(a_lattitude, a_longtitude);
                    Location druga = new Location(b_lattitude, b_longtitude);
                    dlugosc_trasy += Location.CalculateDistance(pierwsza, druga, DistanceUnits.Kilometers);
                }
                var srednia_pr = dlugosc_trasy / (tab_pozycja[i-1]/60);
                if (tab_pozycja[i - 1] < 1)
                {
                    przebyta_droga.Text = "Przebyta droga: " + "zbyt krótka trasa";
                    srednia_predkosc.Text = "Średnia prędkość: " + "zbyt krótka trasa";
                    czas_trasy.Text = "Czas trasy: " + tab_pozycja[i - 1] + " minuty";
                    data.Text = "Data trasy: " + tab_pozycja[i - 1].ToString();
                }
                else
                {
                    przebyta_droga.Text = "Przebyta droga: " + Math.Round(dlugosc_trasy, 2).ToString() + " km";
                    srednia_predkosc.Text = "Średnia prędkość: " + Math.Round(srednia_pr, 2).ToString() + " km/h";
                    czas_trasy.Text = "Czas trasy: " + tab_pozycja[i - 1] + " minuty";
                    data.Text = "Data trasy: " + tab_pozycja[i - 1].ToString();
                }
            }

            maps.MapElements.Add(polyline);
            maps.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(tab_pozycja[0], tab_pozycja[1])
                    , Distance.FromMiles(2)));
      
        }
    }
}