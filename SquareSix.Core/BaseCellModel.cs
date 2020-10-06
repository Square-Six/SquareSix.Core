using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SquareSix.Core
{
    public abstract class BaseCellModel : INotifyPropertyChanged, IDisposable
    {
        public BaseCellModel()
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
