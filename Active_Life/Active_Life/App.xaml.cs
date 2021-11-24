using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Active_Life
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new Button_Page());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
