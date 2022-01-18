using EmailApp.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmailApp.Infrastructure.Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MyBackgroundService(ILogger<MyBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    //Start Inject IEmailService
                    var emailScopeService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    //send email
                    await emailScopeService.TestSendEmail();
                    _logger.LogInformation("From MyBackgroundService : ExecuteAsync (TestSendEmail)");
                    //send every 1 week
                    await Task.Delay(TimeSpan.FromDays(7), stoppingToken);

                }

            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
