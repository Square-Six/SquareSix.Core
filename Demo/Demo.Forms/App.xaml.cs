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

            var configuration = new SetupConfiguration
            {
                iOSAppCenterSecret = "<Your iOS AppCenter Secret>",
                AndroidAppCenterSecret = "<Your Android AppCenter Secret>",
                SecureCacheName = "<Your secure cache name>"
            };
            SquareSixService.Init(configuration);

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
