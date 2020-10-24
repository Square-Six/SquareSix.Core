using System;
using System.Diagnostics;
using Xamarin.Forms;
using SquareSix.Core;

namespace DemoForms.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : SquaredViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                //var item = await DataStore.GetItemAsync(itemId);
                //Id = item.Id;
                //Text = item.Text;
                //Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
