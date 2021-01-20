using System;

namespace SquareSix.Core
{
    public interface ISimpleIOC
    {
        bool ContainsKey<T>();
        void Register<T>(T service);
        void Register<T>() where T : new();
        void Register<T>(Func<T> function);
        void Register(Type type, object service);
        void Register(Type type, Func<object> function);
        void Clear();
        void Unregister<T>();
        T Resolve<T>(bool nullIsAcceptable = false);
        object Resolve(Type type, bool nullIsAcceptable = false);
    }
}
