namespace Transactions.Domain.DTOs
{
    public class AccountDTO
    {
        public int? Id { get; set; }
        public string? Branch { get; set; }
        public string? NumberAccount { get; set; }
        public string? BankName { get; set; }
        public string HolderName { get; set; }
        public string HolderEmail { get; set; }
        public string HolderDocuments { get; set; }
        public string HolderType { get; set; }
        public string TypeAccount { get; set; }
        public string? CodeBank { get; set; }
        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
