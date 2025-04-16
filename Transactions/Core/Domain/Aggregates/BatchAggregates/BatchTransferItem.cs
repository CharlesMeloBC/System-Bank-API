namespace Transactions.Core.Domain.Aggregates.BatchAggregates
{
    public class BatchTransfersItem
    {
        public int Id { get; private set; }
        public string Status { get; private set; }
        public string BankAccountNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }       // accessUser.username
        public string? ApprovedBy { get; private set; }     // accessUser.username

        // Construtor para EF/Core ou outro ORM
        public BatchTransfersItem() { }

        public BatchTransfersItem(string bankAccountNumber, string createdBy)
        {
            BankAccountNumber = bankAccountNumber;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            Status = "PENDING";
        }

        public void Approve(string approvedBy)
        {
            if (Status != "PENDING")
                throw new InvalidOperationException("Apenas lotes pendentes podem ser aprovados.");

            Status = "APPROVED";
            ApprovedBy = approvedBy;
        }
    }

}
