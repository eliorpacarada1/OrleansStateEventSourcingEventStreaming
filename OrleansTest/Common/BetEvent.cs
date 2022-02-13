namespace OrleansTest.Common
{
    [Serializable]
    public class BetEvent
    {
        public Guid ID { get; set; }

        public decimal Amount { get; set; }

        public DateTime? LastUpdated { get; set; }

        public DateTime InsertedAt { get; set; } = DateTime.UtcNow;

        public BetEvent(Guid Id, decimal amount, DateTime? lastUpdated, DateTime InsertedAt)
        {
            ID = ID;
            Amount = amount;
            LastUpdated = lastUpdated;
            this.InsertedAt = InsertedAt;
        }

        public BetEvent()
        {
        }
    }
}
