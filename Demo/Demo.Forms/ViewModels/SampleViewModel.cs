using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Demo.Forms.Interfaces;
using Demo.Forms.Models;
using Demo.Forms.Services;
using Demo.Forms.Views;
using Newtonsoft.Json;
using SquareSix.Core;
using Xamarin.Forms;

namespace Demo.Forms.ViewModels
{
    public class SampleViewModel : BaseListViewModel<SampleCellModel>
    {
        private readonly IAlertService _alertService;
        private readonly IRestService _restService;

        public override string Title => "Samples";

        private AsyncCommand _testAsyncCommand;
        public AsyncCommand TestAsyncCommand => _testAsyncCommand ?? (_testAsyncCommand = new AsyncCommand(RunTestCommandAsync));

        public SampleViewModel()
        {
            _alertService = SimpleIOC.Container.Resolve<IAlertService>();
            _restService = SimpleIOC.Container.Resolve<IRestService>();
        }

        public override Task InitAsync()
        {
            var items = new List<SampleCellModel>();
            items.Add(new SampleCellModel("Alert Sample", AlertSampleAsync));
            items.Add(new SampleCellModel("Alert Confirmation Sample", AlertComfirnationSampleAsync));
            items.Add(new SampleCellModel("Alert Prompt Sample", ShowAlertPromptasync));
            items.Add(new SampleCellModel("ActionSheet example", ShowActionSheetAsync));
            items.Add(new SampleCellModel("Async Command Sample", AsyncCommandTestAsync));
            items.Add(new SampleCellModel("Paging CollectionView Sample", PagingSampleAsync));
            items.Add(new SampleCellModel("Sample Api Request", SampleAPIReuestAsync));
            items.Add(new SampleCellModel("Simple IoC example", SimpleIocExample));

            ListItems = new ObservableCollection<SampleCellModel>(items);

            return base.InitAsync();
        }

        protected override async Task OnItemSelected(object item)
        {
            if (item is SampleCellModel cell)
            {
                cell?.OnCellTappedTask?.Invoke();
            }

            await base.OnItemSelected(item);
        }

        private async Task SampleAPIReuestAsync()
        {
            using (UserDialogs.Instance.Loading())
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                var uriBuilder = new UriBuilder("https://5f7cf91d834b5c0016b05b2f.mockapi.io");
                uriBuilder.AppendUrlSegment("comments");

                var results = await _restService.PrepareAndSendRequest<List<CommentModel>>(HttpMethod.Get, new Uri(uriBuilder.ToString()), null, CancellationToken.None);
                var jsonString = JsonConvert.SerializeObject(results?.Data);
                await _alertService.ShowAlertAsync("Success", jsonString);
            }
        }

        private Task PagingSampleAsync()
        {
            return Shell.Current.GoToAsync(nameof(SamplePagingPage));
        }

        private Task AsyncCommandTestAsync()
        {
            TestAsyncCommand?.ExecuteAsync();
            return Task.CompletedTask;
        }

        private Task AlertSampleAsync()
        {
            return _alertService.ShowAlertAsync("Test", "This is a test alert");
        }

        private async Task AlertComfirnationSampleAsync()
        {
            var result = await _alertService.ShowConfirmationAsync("Are you sure you want to hit the reb button?!", "CONFIRM");
            if (result)
            {
                Debug.WriteLine("Affirmative! Hit the red button");
            }
        }

        private async Task ShowAlertPromptasync()
        {
            var result = await _alertService.ShowPromptAsync("", "Enter some text");
            Debug.WriteLine(result);
        }

        private async Task ShowActionSheetAsync()
        {
            var buttons = new string[] { "Button 1", "Button 2", "Button 3", "Button 4" };
            var result = await _alertService.ShowActionSheetAsync("Super cool title", "Cancel", null, buttons);
            if (result != "Cancel")
            {
                Debug.WriteLine(result);
            }
        }

        private async Task RunTestCommandAsync()
        {
            UserDialogs.Instance.ShowLoading();
            await Task.Delay(TimeSpan.FromSeconds(1));
            UserDialogs.Instance.HideLoading();
            await _alertService.ShowAlertAsync("Async Command", "The command has finished its work");
        }

        private async Task SimpleIocExample()
        {
            // Register Services in the IoC like this
            SimpleIOC.Container.Register<ITestInterface>(new TestInterface());
            // Here is how you retreive the registered services from the IoC
            var testService = SimpleIOC.Container.Resolve<ITestInterface>();

            await testService.ShowTestAlertAsync();
        }
    }
}
