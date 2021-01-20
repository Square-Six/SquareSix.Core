using System;
using System.Threading.Tasks;

namespace SquareSix.Core
{
    public abstract class BaseViewModel : BasePropertyChangedModel
    {
        public bool IsBusy { get; set; }
        public virtual string Title { get; }

        public BaseViewModel()
        {
        }

        public virtual Task InitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
