using System;
using System.Threading.Tasks;

namespace SquareSix.Core
{
    public abstract class BaseViewModel : BaseCellModel
    {
        public bool IsBusy { get; set; }

        protected virtual string Title => string.Empty;

        public BaseViewModel()
        {
        }

        public virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }
    }
}
