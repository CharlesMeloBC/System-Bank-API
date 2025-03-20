using Transactions.Domain.Enums;

namespace Transactions.Domain.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public int BankAccountId { get; set; }
        public string CounterpartyBankCode { get; set; }
        public string CounterpartyBankName { get; set; }
        public string CounterpartyBranch { get; set; }
        public string CounterpartyAccountNumber { get; set; }
        public CounterpartyAccountType CounterpartyAccountType { get; set; }
        public string CounterpartyHolderName { get; set; }
        public CounterpartyHolderType CounterpartyHolderType { get; set; }
        public string CounterpartyHolderDocument { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
