namespace OrleansTest.Common
{
    [Serializable]
    public class AmountState
    {
        public decimal Amount { get; set; }
        public AmountState(decimal amount)
        {
            Amount = amount;
        }
        public AmountState()
        {

        }
    }
}
