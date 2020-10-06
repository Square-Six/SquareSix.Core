using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SquareSix.Core
{
    public abstract class BaseListViewModel<T> : BaseViewModel
    {
        protected virtual bool AutoDeselectItem => true;

        public ObservableCollection<T> ListItems { get; set; }
        public bool IsRefreshing { get; set; }

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
    }
}
