using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FormsSample.Interfaces;
using FormsSample.Models;
using SquareSix.Core;

namespace FormsSample.ViewModels
{
    public class SampleListViewModel : BaseListViewModel<Comment>
    {
        private readonly IComentsService _comentsService;

        protected override string Title => "Sample List";

        public SampleListViewModel()
        {
            _comentsService = SimpleIOC.Container.Resolve<IComentsService>();
        }

        public override async Task OnAppearing()
        {
            IsBusy = true;

            var res = await _comentsService.GetCommentAsync();
            ListItems = new ObservableCollection<Comment>(res);

            IsBusy = false;
        }

        protected override async Task OnGetNextPageAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));

            await base.OnGetNextPageAsync();
        }
    }
}
