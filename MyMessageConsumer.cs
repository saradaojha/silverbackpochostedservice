using MassTransit;
using System.Threading.Tasks;
using System;

namespace Silverback.Samples.Kafka.Batch.Consumer
{
    public class MyMessageConsumer : IConsumer<Batch<MyMessage>>
    {
        public Task Consume(ConsumeContext<Batch<MyMessage>> context)
        {
            var batch = context.Message;



            foreach (var message in batch)
            {
                // Process individual message
                string abcd = message.Message.Text;
                Console.WriteLine($"Received message: {message}");
            }



            return Task.CompletedTask;
        }
    }
}
