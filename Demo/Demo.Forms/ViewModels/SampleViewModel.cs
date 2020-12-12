using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Demo.Forms.Enums;
using Demo.Forms.Models;
using Demo.Forms.Views;
using SquareSix.Core;
using Xamarin.Forms;

namespace Demo.Forms.ViewModels
{
    public class SampleViewModel : SquaredListViewModel<SampleCellModel>
    {
        private readonly ISquaredAlertService _alertService;

        public override string Title => "Samples";

        private AsyncCommand _testAsyncCommand;
        public AsyncCommand TestAsyncCommand => _testAsyncCommand ?? (_testAsyncCommand = new AsyncCommand(RunTestCommandAsync));

        public SampleViewModel()
        {
            _alertService = SimpleIOC.Container.Resolve<ISquaredAlertService>();
        }

        public override Task OnAppearing()
        {
            var items = new List<SampleCellModel>();
            items.Add(new SampleCellModel("Alert Sample", SampleType.Alert));
            items.Add(new SampleCellModel("Async Command Sample", SampleType.AsyncCommand));
            items.Add(new SampleCellModel("Paging CollectionView Sample", SampleType.Paging));

            ListItems = new ObservableCollection<SampleCellModel>(items);

            return base.OnAppearing();
        }

        protected override async Task OnItemSelected(object item)
        {
            if (item is SampleCellModel cell)
            {
                switch(cell.SampleType)
                {
                    case SampleType.Alert:
                        await _alertService.ShowAlertAsync("Test", "This is a test alert");
                        break;
                    case SampleType.AsyncCommand:
                        TestAsyncCommand?.ExecuteAsync();
                        break;
                    case SampleType.Paging:
                        await Shell.Current.GoToAsync(nameof(SamplePagingPage));
                        break;
                }
            }

            await base.OnItemSelected(item);
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
