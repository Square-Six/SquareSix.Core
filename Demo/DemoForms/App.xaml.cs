using System;
using Xamarin.Forms;
using DemoForms.Services;
using SquareSix.Core;
using DemoForms.Interfaces;

namespace DemoForms
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            var config = new SetupConfiguration();
            SquareSixService.Init(config);

            // Register the mock data service to the iOC
            SimpleIOC.Container.Register<IMockApiService>(new MockApiService());

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
