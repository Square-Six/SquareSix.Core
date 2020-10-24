using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SquareSix.Core
{
    public class AlertService : ISquaredAlertService
    {
        public Task ShowAlertAsync(string title, string message, string okText = "OK")
        {
            return Application.Current?.MainPage?.DisplayAlert(title, message, okText);
        }

        public Task ShowPromptAsync(string title, string message, string okText = "OK", string cancel = "Cancel", string placeholder = null, int maxLegnth = -1, Keyboard keyboard = null, string initialValue = null)
        {
            return Application.Current?.MainPage?.DisplayPromptAsync(title, message, okText, cancel, placeholder, maxLegnth, keyboard, initialValue);
        }

        public Task ShowActionSheetAsync(string title, string cancel, string desctruction, params string[] buttons)
        {
            return Application.Current?.MainPage?.DisplayActionSheet(title, cancel, desctruction, buttons);
        }
    }
}
