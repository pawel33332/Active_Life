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
        double[] tab_pozycja = new double[300];

        public Statystyki_Page()
        {
            InitializeComponent();
            Stat_Page.BackgroundColor = Active_Life.Button_Page.tlo_trasy;
            inf.TextColor = Active_Life.Button_Page.text_color;
            przebyta_droga.TextColor = Active_Life.Button_Page.text_color;
            srednia_predkosc.TextColor = Active_Life.Button_Page.text_color;
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
                for (int a = 0; a < i; a += 2)
                {

                    polyline.Geopath.Add(new Position(tab_pozycja[a], tab_pozycja[a + 1]));

                }
            }

            maps.MapElements.Add(polyline);
            maps.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(tab_pozycja[0], tab_pozycja[1])
                    , Distance.FromMiles(2)));
        }
    }
}