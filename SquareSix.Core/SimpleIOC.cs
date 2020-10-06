using System;
using System.Collections.Generic;
using System.Threading;

namespace SquareSix.Core
{
    public static class SimpleIOC
    {
        static readonly Dictionary<Type, Lazy<object>> services = new Dictionary<Type, Lazy<object>>();

        public static void Register<T>(T service)
        {
            services[typeof(T)] = new Lazy<object>(() => service);
        }

        public static void Register<T>() where T : new()
        {
            services[typeof(T)] = new Lazy<object>(() => new T(), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public static void Register<T>(Func<T> function)
        {
            services[typeof(T)] = new Lazy<object>(() => function());
        }

        public static void Register(Type type, object service)
        {
            services[type] = new Lazy<object>(() => service);
        }

        public static void Register(Type type, Func<object> function)
        {
            services[type] = new Lazy<object>(function, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public static void Clear()
        {
            services.Clear();
        }

        public static T Resolve<T>(bool nullIsAcceptable = false) => (T)Resolve(typeof(T), nullIsAcceptable);

        public static object Resolve(Type type, bool nullIsAcceptable = false)
        {
            //Non-scoped services
            {
                Lazy<object> service;

                if (services.TryGetValue(type, out service))
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
}
