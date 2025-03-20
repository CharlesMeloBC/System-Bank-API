using AccountBank.Domain.Enums;

namespace AccountBank.Domain.Models
{
    public class AccountTransactionModel
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; } 
        public DateTime TransactionDate { get; set; }

        public AccountBankModel FromAccount { get; set; }
        public AccountBankModel ToAccount { get; set; }
    }
}
