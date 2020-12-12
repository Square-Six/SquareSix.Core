using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SquareSix.Core;

namespace Demo.Forms.ViewModels
{
    public class PagingVIewModel : SquaredListViewModel<string>
    {
        public override string Title => "Paging Sample";

        public PagingVIewModel()
        {
            ListItems = new ObservableCollection<string>();
        }

        public override async Task OnAppearing()
        {
            var items = await GetMoreItemsAsync();
            ListItems = new ObservableCollection<string>(items);

            await base.OnAppearing();
        }

        protected override async Task OnGetNextPageAsync()
        {
            UserDialogs.Instance.ShowLoading();
            await Task.Delay(TimeSpan.FromSeconds(1));

            var items = await GetMoreItemsAsync();

            var currentItems = ListItems.ToList();
            currentItems.AddRange(items);
            ListItems = new ObservableCollection<string>(currentItems);

            UserDialogs.Instance.HideLoading();

            await base.OnGetNextPageAsync();
        }

        private async Task<List<string>> GetMoreItemsAsync()
        {
            var items = new List<string>();
            var itemCount = ListItems.Count;

            for (var x = 0; x < 30; x++)
            {
                items.Add($"Item {x + itemCount}");
            }

            return items;
        }
    }
}
