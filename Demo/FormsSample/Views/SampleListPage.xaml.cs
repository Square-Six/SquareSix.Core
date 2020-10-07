using System;
using FormsSample.ViewModels;
using Xamarin.Forms;

namespace FormsSample.Views
{
    public partial class SampleListPage : ContentPage
    {
        private readonly SampleListViewModel _viewModel;

        public SampleListPage()
        {
            InitializeComponent();

            if (BindingContext is SampleListViewModel vm)
            {
                _viewModel = vm;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _ = _viewModel?.OnAppearing();
        }
    }
}
