using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using Silverback.Samples.Kafka.Batch.Common;

namespace Silverback.Samples.Kafka.Batch.Consumer
{
    public class TimedHostedService : SampleMessageBatchSubscriber1,IHostedService, IDisposable 
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer? _timer = null;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
        }
        public async Task OnBatchReceivedAsync(IAsyncEnumerable<SampleMessage> batch)
        {
            if (batch == null)
            {
                return;
            }
            int sum = 0;
            int count = 0;
            
            await foreach(var messag in batch)
            {

                string a = messag.CarName;
            }

           await Task.Delay(100);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(20));
          
            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);
            SampleMessageBatchSubscriber1 sm = new SampleMessageBatchSubscriber1();
            await sm.OnBatchReceivedAsync(null);
            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);



        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
