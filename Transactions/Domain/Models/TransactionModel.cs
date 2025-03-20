using Transactions.Domain.Enums;
namespace Transactions.Domain.Models
{
    public class TransactionModel
    {
        public int Id { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public decimal Amount { get; private set; }
        public int BankAccountId { get; private set; }
        public string CounterpartyBankCode { get; private set; }
        public string CounterpartyBankName { get; private set; }
        public string CounterpartyBranch { get; private set; }
        public string CounterpartyAccountNumber { get; private set; }
        public CounterpartyAccountType CounterpartyAccountType { get; private set; }
        public string CounterpartyHolderName { get; private set; }
        public CounterpartyHolderType CounterpartyHolderType { get; private set; }
        public string CounterpartyHolderDocument { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? CreatedAt { get; private set; }

        public TransactionModel(
            TransactionType transactionType,
            decimal amount,
            int bankAccountId,
            string counterpartyBankCode,
            string counterpartyBankName,
            string counterpartyBranch,
            string counterpartyAccountNumber,
            string counterpartyHolderName,
            string counterpartyHolderDocument,
            CounterpartyAccountType counterpartyAccountType,
            CounterpartyHolderType counterpartyHolderType
            )
        {
            TransactionType = transactionType;
            Amount = amount;
            BankAccountId = bankAccountId;
            CounterpartyBankCode = counterpartyBankCode;
            CounterpartyBankName = counterpartyBankName;
            CounterpartyBranch = counterpartyBranch;
            CounterpartyAccountNumber = counterpartyAccountNumber;
            CounterpartyHolderName = counterpartyHolderName;
            CounterpartyHolderDocument = counterpartyHolderDocument;
            CounterpartyAccountType = counterpartyAccountType;
            CounterpartyHolderType = counterpartyHolderType;

            CreatedAt = DateTime.Now;
        }


    }
}
