﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Active_Life
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Kompas : ContentPage
    {
        SensorSpeed _sensorSpeed = SensorSpeed.UI;// Ustawienie opóźnienie prędkości dla zmian monitorowania
        public Kompas()
        {
            InitializeComponent();
            Compass.ReadingChanged += Compass_ReadingChanged; //odczyt czujnika
         
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Compass.Start(_sensorSpeed);  //Rozpocznij monitorowania zmian w kompasie argument predkosc czujnika
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Compass.Stop(); //zatrzymanie monitorowania zmian
        }
        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            
            photoCompass.Rotation =  e.Reading.HeadingMagneticNorth; //obracanie wskaznikiem kompasu
            Compass_info.Text =  e.Reading.HeadingMagneticNorth.ToString(); //konwersja z double do string wypisywanie wartosci
            if (photoCompass.Rotation>=0 && photoCompass.Rotation<90)
            {
                Compass_info.Text = e.Reading.HeadingMagneticNorth.ToString() + " N";
            }
            else if(photoCompass.Rotation >= 90 && photoCompass.Rotation < 180)
            {
                Compass_info.Text = e.Reading.HeadingMagneticNorth.ToString() + " E";
            }
            else if (photoCompass.Rotation >= 180 && photoCompass.Rotation < 270)
            {
                Compass_info.Text = e.Reading.HeadingMagneticNorth.ToString() + " S";
            }
            else if (photoCompass.Rotation >= 270 && photoCompass.Rotation < 360 )
            {
                Compass_info.Text = e.Reading.HeadingMagneticNorth.ToString() + " W";
            }
        }



    }
}
