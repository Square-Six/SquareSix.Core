using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SquareSix.Core
{
    public abstract class SquaredListViewModel<T> : SquaredBaseViewModel
    {
        private bool _isCurrentlyPaging;

        protected virtual bool AutoDeselectItem => true;
        protected virtual bool ShowLoadingOnPaging => true;

        public ObservableCollection<T> ListItems { get; set; }
        public bool IsRefreshing { get; set; }
        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; set; } = 10;

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnItemSelected(value);
            }
        }

        private AsyncCommand _refreshCommand;
        public AsyncCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new AsyncCommand(HandleRefresh));

        private AsyncCommand _thresholdReachedCommand;
        public AsyncCommand ThresholdReachedCommand => _thresholdReachedCommand ?? (_thresholdReachedCommand = new AsyncCommand(HandleThresholdReached));

        protected virtual Task HandleRefresh()
        {
            IsRefreshing = false;
            return Task.CompletedTask;
        }

        protected virtual Task OnItemSelected(object item)
        {
            if (AutoDeselectItem)
            {
                SelectedItem = null;
            }

            return Task.CompletedTask;
        }

        protected virtual Task OnGetNextPageAsync()
        {
            return Task.CompletedTask;
        }

        private async Task HandleThresholdReached()
        {
            if (_isCurrentlyPaging)
            {
                return;
            }

            var isModularPageSize = ListItems.Count % PageSize == 0;
            if (isModularPageSize)
            {
                _isCurrentlyPaging = true;
                CurrentPage++;

                IsBusy = ShowLoadingOnPaging;
                await OnGetNextPageAsync();
                IsBusy = false;

                _isCurrentlyPaging = false;
            }
        }
    }
}
