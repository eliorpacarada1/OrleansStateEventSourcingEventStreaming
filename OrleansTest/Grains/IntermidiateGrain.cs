using Orleans;
using Orleans.Streams;
using OrleansTest.Common;
using OrleansTest.Interfaces;

namespace OrleansTest.Grains
{
    public class IntermidiateGrain : Grain, IIntermidiateGrain
    {
        private IBetGrain currentBet;
        private IAsyncStream<BetMessage> stream;

        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("bet");
            stream = streamProvider.GetStream<BetMessage>(this.GetPrimaryKey(), "default");
            currentBet = GrainFactory.GetGrain<IBetGrain>(this.GetPrimaryKey());
            return base.OnActivateAsync();
        }
        public async Task<AmountState> SetBetAmountAsync(decimal amount)
        {
            await stream.OnNextAsync(new BetMessage(amount, "setBetAmountAsync"));
            AmountState state = new AmountState() { 
                    Amount = amount,
            };
            return await Task.FromResult(state);
        }
    }
}
