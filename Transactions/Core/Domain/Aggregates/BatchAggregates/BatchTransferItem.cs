namespace Transactions.Core.Domain.Aggregates.BatchAggregates
{
    public class BatchTransfersItem
    {
        public Guid Id { get; private set; }
        public TransferType TransferType { get; private set; }
        public long Amount { get; private set; }
        public string BeneficiaryAccountNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int BatchId { get; private set; } 
        public BatchTransfers? Batch {  get; private set; }

        public BatchTransfersItem() { }

        public BatchTransfersItem(long amount, string beneficiaryAccountNumber, TransferType transferType)
        {
            Id = Guid.NewGuid();  // Gerar um GUID único
            Amount = amount;
            BeneficiaryAccountNumber = beneficiaryAccountNumber;
            TransferType = transferType;
            CreatedAt = DateTime.UtcNow;  // Adicionando data de criação
            BatchId = Batch.Id;
            Batch = new BatchTransfers();
        }
    }
}
