using System;
using System.Collections.Generic;

namespace Common.ServiceLocator
{
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> _serviceLocator = new();

        public void Bind<T>(T service)
        {
            Type serviceType = typeof(T);
            
            if (!_serviceLocator.TryAdd(serviceType, service))
                throw new ArgumentException("Service already exists");
        }
        
        public void BindByType(Type serviceType, object service)
        {
            if (!_serviceLocator.TryAdd(serviceType, service))
                throw new ArgumentException("Service already exists");
        }
        
        public T Get<T>()
        {
            Type serviceType = typeof(T);
            
            if (!_serviceLocator.TryGetValue(serviceType, out object service))
                throw new ArgumentException("Service not found");

            return (T) service;
        }
    }
}