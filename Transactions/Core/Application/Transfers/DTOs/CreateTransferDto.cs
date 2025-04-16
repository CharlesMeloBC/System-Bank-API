using Transactions.Core.Domain.Aggregates.BatchAggregates;

namespace Transactions.Core.Aplication.Transfers.DTOs
{
    public class CreateTransferDto
    {
        public long Amount { get; set; }
        public TransferType TransferType { get; set; }
        public string BankAccountNumber { get; set; }
    }

}
