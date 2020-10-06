using System;

namespace SquareSix.Core
{
    public abstract class BaseViewModel : BaseCellModel
    {
        public bool IsBusy { get; set; }
        public string Title { get; set; }

        public BaseViewModel()
        {
        }
    }
}
