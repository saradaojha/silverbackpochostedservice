using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Silverback.Samples.Kafka.Batch.Common;

namespace Silverback.Samples.Kafka.Batch.Consumer
{
    public class SampleMessageBatchSubscriber1
    {
       

        public SampleMessageBatchSubscriber1(
           )
        {
            
        }

        public async Task OnBatchReceivedAsync(IAsyncEnumerable<SampleMessage> batch)
        {
            if (batch == null)
            {
                return;
            }
            int sum = 0;
            int count = 0;
            


          

            await Task.Delay(1000);
        }


    }
}
