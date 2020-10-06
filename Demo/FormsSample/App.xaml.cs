using System;
using Xamarin.Forms;
using FormsSample.Services;
using SquareSix.Core;
using SquareSix.Core.Services;

namespace FormsSample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            SimpleIOC.Register<IAlertService>(new AlertService());

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
