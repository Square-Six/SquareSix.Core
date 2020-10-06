using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using SquareSix.Core;
using System.Threading.Tasks;
using SquareSix.Core.Services;

namespace FormsSample.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;

        private AsyncCommand<string> _testAsyncCommand;
        public AsyncCommand<string> TestAsyncCommand => _testAsyncCommand ?? (_testAsyncCommand = new AsyncCommand<string>(OnTestAsync));

        public Command OpenWebCommand { get; }

        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));

            _alertService = SimpleIOC.Resolve<IAlertService>();
        }

        private async Task OnTestAsync(string value)
        {
            await _alertService.ShowAlertAsync("Alert", "Testing Alert!");
        }
    }
}