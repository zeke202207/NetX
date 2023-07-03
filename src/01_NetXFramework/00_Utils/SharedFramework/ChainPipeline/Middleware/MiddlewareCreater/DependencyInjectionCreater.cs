using System;
using System.Collections.Generic;
using System.Text;

namespace NetX.SharedFramework.ChainPipeline
{
    public class DependencyInjectionCreater : IMiddlewareCreater
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjectionCreater(IServiceProvider service)
        {
            _serviceProvider = service;
        }

        public object Create(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
