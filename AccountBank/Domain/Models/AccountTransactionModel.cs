using AccountBank.Domain.Enums;

namespace AccountBank.Domain.Models
{
    public class AccountTransactionModel
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public int BankAccountId { get; set; }
        public string CounterpartyBankCode { get; set; }
        public string CounterpartyBankName { get; set; }
        public string CounterpartyBranch { get; set; }
        public string CounterpartyAccountNumber { get; set; }
        public string CounterpartyAccountType { get; set; }
        public string CounterpartyHolderName { get; set; }
        public string CounterpartyHolderType { get; set; }
        public string CounterpartyHolderDocument { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
