using System;
using System.Collections.Generic;
using System.Threading;

namespace SquareSix.Core
{
    public class SimpleIOCContainer : ISimpleIOC
    {
        static readonly Dictionary<Type, Lazy<object>> services = new Dictionary<Type, Lazy<object>>();

        public bool ContainsKey<T>()
        {
            return services.ContainsKey(typeof(T));
        }

        public void Register<T>(T service)
        {
            services[typeof(T)] = new Lazy<object>(() => service);
        }

        public void Register<T>() where T : new()
        {
            services[typeof(T)] = new Lazy<object>(() => new T(), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public void Register<T>(Func<T> function)
        {
            services[typeof(T)] = new Lazy<object>(() => function());
        }

        public void Register(Type type, object service)
        {
            services[type] = new Lazy<object>(() => service);
        }

        public void Register(Type type, Func<object> function)
        {
            services[type] = new Lazy<object>(function, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public void Clear()
        {
            services.Clear();
        }

        public void Unregister<T>()
        {
            bool keyExists = services.TryGetValue(typeof(T), out _);
            if (keyExists)
            {
                services.Remove(typeof(T));
            }
        }

        public T Resolve<T>(bool nullIsAcceptable = false) => (T)Resolve(typeof(T), nullIsAcceptable);

        public object Resolve(Type type, bool nullIsAcceptable = false)
        {

            if (services.TryGetValue(type, out Lazy<object> service))
            {
                return service.Value;
            }

            if (nullIsAcceptable)
            {
                return null;
            }

            throw new KeyNotFoundException($"Service not found for type '{type}'");
        }
    }
}