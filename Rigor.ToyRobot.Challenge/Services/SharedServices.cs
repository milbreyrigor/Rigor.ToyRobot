using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Services
{
    /// <summary>
    /// Holds Shared Services.
    /// </summary>
    public static class SharedServices
    {

        private static Dictionary<Type, object> Services { get; set; }

        private static object LockServices { get; set; }

        /// <summary>
        /// Initializes static members of the <see cref="SharedServices"/> class.
        /// </summary>
        static SharedServices()
        {
            Services = new Dictionary<Type, object>();
            LockServices = new object();
        }

        /// <summary>
        /// Determines whether the specified type has service.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type has service; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasService(Type type)
        {
            bool result = Services.ContainsKey(type);
            return result;
        }

        /// <summary>
        /// Sets the service.
        /// </summary>
        /// <typeparam name="T">The Type of Service to set</typeparam>
        /// <param name="service">An instance of the service.</param>
        public static void SetService<T>(T service) where T : class
        {
            lock (LockServices)
            {
                Services[typeof(T)] = service;
            }
        }

        public static void SetService(object service, Type type)
        {

            lock (LockServices)
            {
                Services[type] = service;
            }
        }

        /// <summary>
        /// Gets the service of the specified Type.
        /// </summary>
        /// <typeparam name="T">The Type of Service to get</typeparam>
        /// <returns>The instance of the service for the specified Type or null if the service does not exist.</returns>
        public static T GetService<T>() where T : class
        {
            lock (LockServices)
            {
                if (Services.ContainsKey(typeof(T)))
                {
                    T service = Services[typeof(T)] as T;
                    return service;
                }
                else
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// Destroys all services.
        /// </summary>
        public static void DestroyAllServices()
        {
            lock (LockServices)
            {

                foreach (var service in Services)
                {
                    try
                    {
                        if (service.Value is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                        else
                        {
                            Services[service.Key] = null;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                Services.Clear();
            }
        }
    }
}
