using System;
using System.Threading.Tasks;

namespace SquareSix.Core
{
    public abstract class SquaredViewModel : SquaredBaseCellModel
    {
        public bool IsBusy { get; set; }

        protected virtual string Title => string.Empty;

        public SquaredViewModel()
        {
        }

        public virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }
    }
}
