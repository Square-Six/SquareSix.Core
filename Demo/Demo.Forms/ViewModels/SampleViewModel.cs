using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Demo.Forms.Models;
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
            items.Add(new SampleCellModel("Async Command Sample", AsyncCommandTestAsync));
            items.Add(new SampleCellModel("Paging CollectionView Sample", PagingSampleAsync));
            items.Add(new SampleCellModel("Sample Api Request", SampleAPIReuestAsync));

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

        private async Task RunTestCommandAsync()
        {
            UserDialogs.Instance.ShowLoading();
            await Task.Delay(TimeSpan.FromSeconds(1));
            UserDialogs.Instance.HideLoading();
            await _alertService.ShowAlertAsync("Async Command", "The command has finished its work");
        }
    }
}
