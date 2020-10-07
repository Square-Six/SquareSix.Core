using System;
using Xamarin.Forms;
using FormsSample.Services;
using SquareSix.Core;
using SquareSix.Core.Services;
using FormsSample.Interfaces;

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
