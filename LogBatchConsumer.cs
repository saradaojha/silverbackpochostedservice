using MassTransit;
using System.Text;
using System.Threading.Tasks;

namespace Silverback.Samples.Kafka.Batch.Consumer
{
   public class LogBatchConsumer :IConsumer<Batch<KafkaMessage>>
    {
        public async Task Consume(ConsumeContext<Batch<KafkaMessage>> context)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < context.Message.Length; i++)
            {
                builder.Append(context.Message[i].Message.CarName);
            }


            string abcd = builder.ToString();

            await Task.Delay(2000);        
            // do something with the string, like write it to a file
        }
    }
}
