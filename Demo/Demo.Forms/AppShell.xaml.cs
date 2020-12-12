using System;
using Demo.Forms.Views;
using Xamarin.Forms;

namespace Demo.Forms
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SamplePagingPage), typeof(SamplePagingPage));
        }

    }
}
