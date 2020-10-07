using System;
using SquareSix.Core;
using System.Threading.Tasks;
using SquareSix.Core.Services;

namespace FormsSample.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly ISecureCacheService _secureCacheService;

        private AsyncCommand<string> _testAsyncCommand;
        public AsyncCommand<string> TestAsyncCommand => _testAsyncCommand ?? (_testAsyncCommand = new AsyncCommand<string>(OnTestAsync));

        public AboutViewModel()
        {
            Title = "About";

            _alertService = SimpleIOC.Container.Resolve<IAlertService>();

            _secureCacheService = SimpleIOC.Container.Resolve<ISecureCacheService>();
            _secureCacheService.Setup("App-SecureCache");
        }

        private async Task OnTestAsync(string value)
        {
            await _alertService.ShowAlertAsync("Alert", "Testing Alert!");
        }
    }
}