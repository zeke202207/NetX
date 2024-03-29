﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;

namespace NetX.App
{
    public class AppActionDescriptorChangeProvider : IActionDescriptorChangeProvider
    {
        private static Lazy<AppActionDescriptorChangeProvider> _instance = new Lazy<AppActionDescriptorChangeProvider>(() => new AppActionDescriptorChangeProvider());

        public static AppActionDescriptorChangeProvider Instance => _instance.Value;

        public CancellationTokenSource TokenSource { get; private set; }

        public bool HasChanged { get; set; }

        public IChangeToken GetChangeToken()
        {
            TokenSource = new CancellationTokenSource();
            return new CancellationChangeToken(TokenSource.Token);
        }
    }
}
