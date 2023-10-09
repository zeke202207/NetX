﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetX.App;

sealed class AppHostedService : BackgroundService
{
    private readonly ILogger<AppHostedService> logger;

    public AppHostedService(ILogger<AppHostedService> logger)
    {
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested == false)
        {
            await Task.Delay(TimeSpan.FromSeconds(1d), stoppingToken);
            this.logger.LogInformation(DateTimeOffset.Now.ToString());
        }
    }
}
