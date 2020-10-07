using System;
using Xamarin.Forms;
using FormsSample.Services;
using SquareSix.Core;
using SquareSix.Core.Services;
using FormsSample.Interfaces;
using SquareSix.Core.Models;

namespace FormsSample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            SimpleIOC.Container.Register<IAlertService>(new AlertService());
            SimpleIOC.Container.Register<IComentsService>(new ComentsService());

            var config = new SetupConfiguration
            {
                UseAlertService = true,
                UseAnalyticsService = true,
                UseSecureCacheService = true,
                iOSAppCenterSecret = "2342q42432q34",
                AndroidAppCenterSecret = "2352452q54q325q25",
                SecureCacheName = "my-app-cache"
            };
            Bootstrap.Initialize(config);

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
