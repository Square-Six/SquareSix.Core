using System;
using SquareSix.Core;
using Xamarin.Forms;

namespace Demo.Forms
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            SquareSixService.Init();

            MainPage = new AppShell();
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
