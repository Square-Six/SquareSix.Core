using System;
using System.Threading.Tasks;
using SquareSix.Core.Models;

namespace SquareSix.Core
{
    public abstract class SquaredBaseViewModel : BasePropertyChangedModel
    {
        public bool IsBusy { get; set; }
        public virtual string Title { get; }

        public SquaredBaseViewModel()
        {
        }

        public virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }
    }
}
