using Orleans.Streams;
using OrleansTest.Common;

namespace OrleansTest.Observers
{
    public class StreamObserver : IAsyncObserver<BetMessage>
    {
        public Task OnCompletedAsync() => Task.CompletedTask;


        public Task OnErrorAsync(Exception ex) => Task.CompletedTask;
        

        public Task OnNextAsync(BetMessage item, StreamSequenceToken token = null)
        {
            Console.WriteLine($"Qa bone, qiky stream erdh sefte {item.Amount}, {item.InsertedAt}, {item.ID}");
            return Task.CompletedTask;
        }
    }
}
