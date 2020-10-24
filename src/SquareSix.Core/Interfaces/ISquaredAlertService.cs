using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SquareSix.Core
{
    public interface ISquaredAlertService
    {
        Task ShowAlertAsync(string title, string message, string okText = "OK");
        Task ShowPromptAsync(string title, string message, string okText = "OK", string cancel = "Cancel", string placeholder = null, int maxLegnth = -1, Keyboard keyboard = null, string initialValue = null);
        Task ShowActionSheetAsync(string title, string cancel, string desctruction, params string[] buttons);
    }
}
