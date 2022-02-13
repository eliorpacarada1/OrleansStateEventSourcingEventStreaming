using Orleans;
using Orleans.EventSourcing;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Streams;
using OrleansTest.Common;
using OrleansTest.Interfaces;

namespace OrleansTest.Grains
{
    [StorageProvider(ProviderName = "orleansStorage")]
    [LogConsistencyProvider(ProviderName = "testLogStorage")]
    [ImplicitStreamSubscription("default")]
    public class BetGrain : JournaledGrain<AmountState, BetEvent>, IBetGrain
    {
        private readonly IPersistentState<AmountState> _amountState;
        private readonly IAsyncObservable<BetMessage> _consumer;
      
        public BetGrain([PersistentState("amountstorage", "orleansStorage")] IPersistentState<AmountState> amountState)
        {
            _amountState = amountState;
        }
        public Task<string> SayHello(string greetings) =>
               Task.FromResult($"Hello {greetings}");
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("bet");
            var stream = streamProvider.GetStream<BetMessage>(this.GetPrimaryKey(), "default");
            await stream.SubscribeAsync(OnNextAsync, OnErrorAsync, OnCompletedAsync);
        }
        public async Task<decimal> SetAmountAsync(decimal amount)
        {
            _amountState.State.Amount = amount;
            RaiseEvent(new BetEvent() { ID = Guid.NewGuid(), Amount = amount, InsertedAt = DateTime.UtcNow, LastUpdated = DateTime.UtcNow });
            await ConfirmEvents();
            await _amountState.WriteStateAsync();
            return await Task.FromResult(amount);
        }
        public async Task OnNextAsync(BetMessage item, StreamSequenceToken token = null)
        {
            await SetAmountAsync(item.Amount);
            await Task.CompletedTask;
        }
        public Task OnErrorAsync(Exception ex) => Task.CompletedTask;

        public Task OnCompletedAsync() => Task.CompletedTask;
    }
}
