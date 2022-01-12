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
    public partial class Trasa : ContentPage
    {
        
        string trasa_file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "path.txt");
        string trasa_file_stat1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path.txt");
        string trasa_file_stat2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path2.txt");
        string trasa_file_stat3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path3.txt");
        string trasa_file_stat4 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path4.txt");
        string trasa_file_stat5 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "last_path5.txt");
        string wartosc;
        DateTime data_start;
        DateTime data_koniec;
        double[] tab_pozycja = new double[1000];
     

        public Trasa()
        {
            InitializeComponent();
            Trasa_Page.BackgroundColor = Active_Life.Button_Page.tlo_trasy;
            obecna_pozycja();
            //odczytaj_trase();

        }
        async private void Wyswietl_statystyki(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statystyki_Page());

        }
        async private void obecna_pozycja()
        {
            
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(2));
            var location = await Geolocation.GetLocationAsync(request);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude)
            , Distance.FromMiles(2)));
             odczytaj_trase();
            
        }
        async private void Sledz_trase(object sender, EventArgs e)
        {
            int a = Preferences.Get("trasa_aktywna", 0);
            if (a == 0)
            {
                File.WriteAllText(trasa_file, String.Empty);
                data_start = DateTime.Now;
                await DisplayAlert("Informacja", "Rozpoczêto œledzenie trasy.", "OK");
                sledzenie();
                Preferences.Set("trasa_aktywna", 1);
            } else
            {
             await DisplayAlert("Informacja", "Nie mo¿na rozpocz¹æ nowej trasy. Musisz zakoñczyæ obecn¹", "OK");
            }

        }
        async private void sledzenie()
        {
            
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(2));
            var location = await Geolocation.GetLocationAsync(request);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude)
            , Distance.FromMiles(2)));
           
            using (StreamWriter sw = File.AppendText(trasa_file))
            {
                sw.WriteLine(location.Latitude);
                sw.WriteLine(location.Longitude);

            }
           using (StreamReader sr = new StreamReader(trasa_file))
            {

                while ((wartosc = sr.ReadLine()) == null)
                {
                    tab_pozycja[0] = location.Latitude;
                    tab_pozycja[1] = location.Longitude;

                    string[] lines =
                    {
                          tab_pozycja[0].ToString(),tab_pozycja[1].ToString()
                        };
                    File.WriteAllLines(trasa_file, lines);

                }

            }
            
            odczytaj_trase();
        }
         async private void odczytaj_trase()
        {
            if (!File.Exists(trasa_file))
            {
                return;
            }

                Polyline polyline = new Polyline
            {
                StrokeWidth = 20,
                StrokeColor = Color.FromHex("#dd9ecd"),

                Geopath =
            {


            }
            };
            using (StreamReader sr = new StreamReader(trasa_file))
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
             for (int a = 0; a <i; a+=2)
            {

                polyline.Geopath.Add(new Position(tab_pozycja[a], tab_pozycja[a+1]));

            }
            }
              
            map.MapElements.Add(polyline);
            if(Preferences.Get("trasa_aktywna", 0) == 1)
            {
            await Task.Delay(10000);
            sledzenie();
            }
            
        }
        
        async private void Koniec_trasy(object sender, EventArgs e)
        {
            if(Preferences.Get("trasa_aktywna",0)==1)
            {
            data_koniec = DateTime.Now;
            var czas = (data_koniec - data_start).Minutes;
          
            if (File.Exists(trasa_file_stat4))
                {
                    File.WriteAllText(trasa_file_stat5, "");
                    using (var reader = new StreamReader(trasa_file_stat4))
                    using (var writer = new StreamWriter(trasa_file_stat5))
                    {
                      
                        writer.Write(reader.ReadToEnd());

                    }
            }
          if (File.Exists(trasa_file_stat3))
                {
                    File.WriteAllText(trasa_file_stat4, "");
                    using (var reader = new StreamReader(trasa_file_stat3))
                    using (var writer = new StreamWriter(trasa_file_stat4))
                    {
                        
                        writer.Write(reader.ReadToEnd());

                    }
                }
           if (File.Exists(trasa_file_stat2))
                {
                    File.WriteAllText(trasa_file_stat3, "");
                    using (var reader = new StreamReader(trasa_file_stat2))
                    using (var writer = new StreamWriter(trasa_file_stat3))
                    {
                        
                        writer.Write(reader.ReadToEnd());

                    }
                }
            if (File.Exists(trasa_file_stat1))
                {
                    File.WriteAllText(trasa_file_stat2, "");
                    using (var reader = new StreamReader(trasa_file_stat1))
                    using (var writer = new StreamWriter(trasa_file_stat2))
                    {
                        
                        writer.Write(reader.ReadToEnd());

                    }
                }
               File.WriteAllText(trasa_file_stat1, "");
                    using (var reader = new StreamReader(trasa_file))
                    using (var writer = new StreamWriter(trasa_file_stat1))
                    {
                    
                    writer.Write(reader.ReadToEnd());
                    // writer.Write(data_start.ToString("yyyy")+data_start.ToString("MM")+data_start.ToString("DD"));
                    // writer.WriteLine();
                     writer.Write(czas);
                    
                    }

               
            Preferences.Set("trasa_aktywna", 0);
            File.WriteAllText(trasa_file, "");
            await Navigation.PushAsync(new Trasa());
            await DisplayAlert("Informacja", "Zakoñczono trasê. Przejdz do zak³adki statystyki aby zobaczyæ ostatni¹ trasê z zebranymi informacjami.", "OK");
            
            } else
            {
           await DisplayAlert("Informacja", "Nie mo¿na zakoñczyæ trasy, poniewa¿ nie zosta³a rozpoczêta", "OK");
            }
            
            

        }
      


    }
}