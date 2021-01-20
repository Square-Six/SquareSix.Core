using System;
using Demo.Forms.ViewModels;
using Xamarin.Forms;

namespace Demo.Forms.Views
{
    public partial class SamplePage : ContentPage
    {
        public SamplePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is SampleViewModel vm)
            {
                _ = vm.InitAsync();
            }
        }
    }
}
