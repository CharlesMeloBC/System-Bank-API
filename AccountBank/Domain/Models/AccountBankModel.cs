using AccountBank.Domain.Enums;

namespace AccountBank.Domain.Models
{
    public class AccountBankModel
    {
        public int Id { get; private set; }
        public string? Branch { get; private set; }
        public string? NumberAccount { get; private set; }
        public AccountType TypeAccount { get; private set; }
        public string HolderName { get; private set; } = null!;
        public string HolderEmail { get; private set; }
        public string HolderDocuments { get; private set; }
        public HolderType HolderType { get; private set; }
        public AccountStatus? Status { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public string CodeBank { get; private set; }
        public string BankName { get; private set; }

        public BalanceModel Balance { get; set;}

        private AccountBankModel() { }

        public AccountBankModel(
            string holderName, 
            AccountType accountType,
            string holderEmail,
            string holderDocuments,
            HolderType holderType
            )
        {

        var account = new AccountBankModel();

           {
            Branch = CreateNumberBranch();
            BankName = "DelFinance"; 
            CodeBank = "435";
            NumberAccount = CreateNumberAccount();
            TypeAccount = accountType;
            HolderName = holderName;
            HolderEmail = holderEmail;
            HolderDocuments = holderDocuments;
            HolderType = holderType;
            Status = AccountStatus.ACTIVE;
            CreatedAt = DateTime.Now;
            }
        
            account.Balance = new BalanceModel(account.Id);
                   
        } 
        private static string CreateNumberAccount()
        {
            var random = new Random();
            int length = random.Next(5, 10);

            string NumberAcc = "";

            for (int i = 0; i < length; i++)
            {
                NumberAcc += random.Next(0, 10).ToString();
            }
            return NumberAcc;
        }

        private static string CreateNumberBranch() 
        {
            var random = new Random();

            var StringBranch = "";

            for (int i = 0; i < 5; i++)
            {
               StringBranch += random.Next(0, 10).ToString();
            }
            return StringBranch;
        }

        public void ChangeAccountStatus(AccountStatus newState)
        {
            Status = newState;


            if 
               (
                newState == AccountStatus.FINISHED || 
                newState == AccountStatus.BLOCKED  || 
                newState == AccountStatus.ACTIVE
                )
            {
                UpdatedAt = DateTime.UtcNow;
            }
        }
        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("O e-mail não pode ser vazio.");

            HolderEmail = newEmail;
            UpdatedAt = DateTime.UtcNow;
        }
        public void UpdateHolderName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("O nome não pode ser vazio.");

            HolderName = newName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
