using System;
using SquareSix.Core;
using System.Threading.Tasks;
using DemoForms.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DemoForms.Models;

namespace DemoForms.ViewModels
{
    public class AboutViewModel : SquaredListViewModel<SquaredBaseCellModel>
    {
        private readonly ISquaredAlertService _alertService;
        private readonly IMockApiService _mockApiService;

        protected override string Title => "About";

        private AsyncCommand _testAsyncCommand;
        public AsyncCommand TestAsyncCommand => _testAsyncCommand ?? (_testAsyncCommand = new AsyncCommand(TestAsyncCommandAsync));

        private AsyncCommand _apiCommand;
        public AsyncCommand ApiCommand => _apiCommand ?? (_apiCommand = new AsyncCommand(ApiCommandAsync));

        public AboutViewModel()
        {
            _alertService = SimpleIOC.Container.Resolve<ISquaredAlertService>();
            _mockApiService = SimpleIOC.Container.Resolve<IMockApiService>();
        }

        private Task TestAsyncCommandAsync()
        {
            return _alertService.ShowAlertAsync("Async Command", "This was run asynchronously!");
        }

        private async Task ApiCommandAsync()
        {
            if (ListItems?.Any() ?? false)
            {
                ListItems.Clear();
            }

            var items = new List<SquaredBaseCellModel>();
            var comments = await _mockApiService.GetCommentModelsAsync();
            foreach(var c in comments)
            {
                items.Add(c);
            }

            ListItems = new ObservableCollection<SquaredBaseCellModel>(items);
        }

        protected override async Task OnItemSelected(object item)
        {
            if (item is CommentModel commentModel)
            {
                await _alertService.ShowAlertAsync("", commentModel.Name);
            }

            await base.OnItemSelected(item);
        }
    }
}