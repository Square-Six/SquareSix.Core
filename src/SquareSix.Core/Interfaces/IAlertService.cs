using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SquareSix.Core
{
    public interface IAlertService
    {
        Task<bool> ShowConfirmationAsync(string message, string title = "", string okText = "OK", string cancel = "Cancel");
        Task ShowAlertAsync(string title, string message, string okText = "OK");
        Task<string> ShowPromptAsync(string title, string message, string okText = "OK", string cancel = "Cancel", string placeholder = null, int maxLegnth = -1, Keyboard keyboard = null, string initialValue = null);
        Task<string> ShowActionSheetAsync(string title, string cancel, string desctruction, params string[] buttons);
    }
}
