using MassTransit;
using System.Threading.Tasks;

namespace Silverback.Samples.Kafka.Batch.Consumer
{

    class KafkaMessageConsumer :
        IConsumer<KafkaMessage>
    {
        public Task Consume(ConsumeContext<KafkaMessage> context)
        {
            return Task.CompletedTask;
        }
    }
    public record KafkaMessage
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string BookingStatus { get; set; }
    }
}
