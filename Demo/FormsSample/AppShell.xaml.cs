using System;
using System.Collections.Generic;
using FormsSample.ViewModels;
using FormsSample.Views;
using Xamarin.Forms;

namespace FormsSample
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
