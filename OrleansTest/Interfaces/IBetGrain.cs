using Orleans;
using OrleansTest.Common;

namespace OrleansTest.Interfaces
{
    public interface IBetGrain : IGrainWithGuidKey
    {
        Task<string> SayHello(string greetings);

        Task<decimal>SetAmountAsync(decimal amount);
    }
}
