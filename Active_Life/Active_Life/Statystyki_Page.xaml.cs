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
        string trasa_file_stat1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path.txt");
        string trasa_file_stat2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path2.txt");
        string trasa_file_stat3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path3.txt");
        string trasa_file_stat4 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path4.txt");
        string trasa_file_stat5 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path5.txt");
        string wartosc;
        double[] tab_pozycja = new double[1000];
        double dlugosc_trasy = 0;
        int nr_trasy = 1;


        public Statystyki_Page()
        {
            InitializeComponent();
            Stat_Page.BackgroundColor = Active_Life.Button_Page.tlo_trasy;
            inf.TextColor = Active_Life.Button_Page.text_color;
            przebyta_droga.TextColor = Active_Life.Button_Page.text_color;
            srednia_predkosc.TextColor = Active_Life.Button_Page.text_color;
            czas_trasy.TextColor = Active_Life.Button_Page.text_color;
            data.TextColor = Active_Life.Button_Page.text_color;
            if (File.Exists(trasa_file_stat1))
            {
                inf.Text = "Statystyki twojej ostatniej trasy";
                odczytaj_trase(trasa_file_stat1);
            }
            else
            {
                inf.Text = "Nie masz żadnej zapisanej trasy";
            }

        }
        private void odczytaj_trase(string nr_trasy)
        {
            Polyline polyline = new Polyline
            {
                StrokeWidth = 20,
                StrokeColor = Color.FromHex("#dd9ecd"),

                Geopath =
            {


            }
            };


            using (StreamReader sr = new StreamReader(nr_trasy))
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
                for (int a = 0; a < i - 2; a += 2)
                {

                    polyline.Geopath.Add(new Position(tab_pozycja[a], tab_pozycja[a + 1]));

                }
                for (int a = 0; a <= i - 3; a += 4)
                {
                    var a_lattitude = tab_pozycja[a];
                    var a_longtitude = tab_pozycja[a + 1];
                    var b_lattitude = tab_pozycja[a + 2];
                    var b_longtitude = tab_pozycja[a + 3];
                    Location pierwsza = new Location(a_lattitude, a_longtitude);
                    Location druga = new Location(b_lattitude, b_longtitude);
                    dlugosc_trasy += Location.CalculateDistance(pierwsza, druga, DistanceUnits.Kilometers);
                }
                var srednia_pr = dlugosc_trasy / (tab_pozycja[i - 1] / 60);
                if (tab_pozycja[i - 1] < 1)
                {
                    przebyta_droga.Text = "Przebyta droga: " + "zbyt krótka trasa";
                    srednia_predkosc.Text = "Średnia prędkość: " + "zbyt krótka trasa";
                    czas_trasy.Text = "Czas trasy: " + tab_pozycja[i - 1] + " minuty";
                    data.Text = "Data trasy: " + tab_pozycja[i - 2];
                }
                else
                {
                    przebyta_droga.Text = "Przebyta droga: " + Math.Round(dlugosc_trasy, 2).ToString() + " km";
                    srednia_predkosc.Text = "Średnia prędkość: " + Math.Round(srednia_pr, 2).ToString() + " km/h";
                    czas_trasy.Text = "Czas trasy: " + tab_pozycja[i - 1] + " minuty";
                    data.Text = "Data trasy: " + tab_pozycja[i - 2];
                }
            }

            maps.MapElements.Add(polyline);
            maps.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(tab_pozycja[0], tab_pozycja[1])
                    , Distance.FromMiles(2)));

        }

        void OnSwiped(object sender, SwipedEventArgs e)
        {
            
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    maps.MapElements.Clear();
                    nr_trasy--;
                    if(nr_trasy==4)
                    {
                        if (File.Exists(trasa_file_stat4))
                        {
                            inf.Text = "Statystyki twojej czwartej trasy";
                            odczytaj_trase(trasa_file_stat4);
                        }
                        
                    } else if(nr_trasy == 3) {
                        if (File.Exists(trasa_file_stat4))
                        {
                            inf.Text = "Statystyki twojej trzeciej trasy";
                            odczytaj_trase(trasa_file_stat3);
                        }
                    }
                    else if (nr_trasy == 2)
                    {
                        if (File.Exists(trasa_file_stat2))
                        {
                            inf.Text = "Statystyki twojej drugiej trasy";
                            odczytaj_trase(trasa_file_stat2);
                        }
                    }
                    else if (nr_trasy == 1)
                    {
                        if (File.Exists(trasa_file_stat1))
                        {
                            inf.Text = "Statystyki twojej ostatniej trasy";
                            odczytaj_trase(trasa_file_stat1);
                        }
                    }
                    else if (nr_trasy < 1)
                    {
                        inf.Text = "Statystyki twojej ostatniej trasy";
                        nr_trasy = 1;
                        odczytaj_trase(trasa_file_stat1);
                    }
                    break;
                case SwipeDirection.Right:
                    maps.MapElements.Clear();
                    nr_trasy++;
                    if (nr_trasy == 2)
                    {
                        if (File.Exists(trasa_file_stat2))
                        {
                            inf.Text = "Statystyki twojej drugiej trasy";
                            odczytaj_trase(trasa_file_stat2);
                        }

                    }
                    else if (nr_trasy == 3)
                    {
                        if (File.Exists(trasa_file_stat3))
                        {
                            inf.Text = "Statystyki twojej trzeciej trasy";
                            odczytaj_trase(trasa_file_stat3);
                        }
                    }
                    else if (nr_trasy == 4)
                    {
                        if (File.Exists(trasa_file_stat4))
                        {
                            inf.Text = "Statystyki twojej czwartej trasy";
                            odczytaj_trase(trasa_file_stat4);
                        }
                    }
                    else if (nr_trasy == 5)
                    {
                        if (File.Exists(trasa_file_stat5))
                        {
                            inf.Text = "Statystyki twojej piątej trasy";
                            odczytaj_trase(trasa_file_stat5);
                        }
                    }
                    else if (nr_trasy > 5)
                    {
                        inf.Text = "Statystyki twojej piątej trasy";
                        odczytaj_trase(trasa_file_stat5);
                        nr_trasy = 5;
                    }
                    break;
            
            }
            

        }
    }
}