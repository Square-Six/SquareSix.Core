using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SquareSix.Core.Models
{
    public abstract class BasePropertyChangedModel : INotifyPropertyChanged, IDisposable
    {
        public BasePropertyChangedModel()
        {
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
