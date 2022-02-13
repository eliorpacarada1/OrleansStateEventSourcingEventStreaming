namespace OrleansTest.Common
{
    [Serializable]
    public class BetMessage
    {
        private string v;

        public BetMessage(decimal amount, string v)
        {
            Amount = amount;
            this.v = v;
        }

        public Guid ID { get; set; }

        public decimal Amount { get; set; }

        public DateTime? LastUpdated { get; set; }

        public DateTime InsertedAt { get; set; } = DateTime.UtcNow;
    }
}
