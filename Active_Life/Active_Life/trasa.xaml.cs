using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Xamarin.Essentials;

namespace Active_Life
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Trasa : ContentPage
    {
        public int zakoncz_trase = 0;
        public Trasa()
        {
            InitializeComponent();
            Trasa_Page.BackgroundColor = Active_Life.Button_Page.tlo_trasy;
            Sledz_trase(null, EventArgs.Empty);
        }
        async void Sledz_trase(object sender, EventArgs e)
        {
            zakoncz_trase = 0;
            while(zakoncz_trase==0)
            {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(request);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude)
            ,Distance.FromMiles(1)));
            Polygon polygon = new Polygon
            {
                StrokeWidth = 10,
                StrokeColor = Color.FromHex("#dd9ecd"),
                FillColor = Color.FromHex("#800080"),
                Geopath =
            {
              new Position(Preferences.Get("szerokosc_geo",location.Latitude),Preferences.Get("dlugosc_geo",location.Longitude)),
              new Position(location.Latitude, location.Longitude)
            }
            };
            map.MapElements.Add(polygon);
            Preferences.Set("szerokosc_geo", location.Latitude);
            Preferences.Set("dlugosc_geo", location.Longitude);
                
            }
            }
      
         

    }  
}