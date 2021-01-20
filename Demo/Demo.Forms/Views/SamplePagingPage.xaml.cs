using System;
using Demo.Forms.ViewModels;
using Xamarin.Forms;

namespace Demo.Forms.Views
{
    public partial class SamplePagingPage : ContentPage
    {
        public SamplePagingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is PagingVIewModel vm)
            {
                _ = vm.InitAsync();
            }
        }
    }
}
