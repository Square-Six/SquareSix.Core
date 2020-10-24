using System.ComponentModel;
using Xamarin.Forms;
using DemoForms.ViewModels;

namespace DemoForms.Views
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