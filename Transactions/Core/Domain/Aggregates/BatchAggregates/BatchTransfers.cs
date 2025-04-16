namespace Transactions.Core.Domain.Aggregates.BatchAggregates
{
    public class BatchTransfers
    {
        public Guid Id { get; private set; }
        public int Batch { get; private set; }
        public TransferType TransferType { get; private set; }
        public long Amount { get; private set; }
        public string BeneficiaryAccountNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public BatchTransfers() { }

        public BatchTransfers(long amount, string beneficiaryAccountNumber, TransferType transferType)
        {
            Id = Guid.NewGuid();  // Gerar um GUID único
            Batch = 1; // Se você quer um valor fixo ou vai configurar em outro lugar
            Amount = amount;
            BeneficiaryAccountNumber = beneficiaryAccountNumber;
            TransferType = transferType;
            CreatedAt = DateTime.UtcNow;  // Adicionando data de criação
        }
    }
}
