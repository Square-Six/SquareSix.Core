using System.ComponentModel;
using Xamarin.Forms;
using FormsSample.ViewModels;

namespace FormsSample.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}