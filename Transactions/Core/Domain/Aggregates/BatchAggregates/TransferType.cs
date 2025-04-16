using System.ComponentModel.DataAnnotations;

namespace Transactions.Core.Domain.Aggregates.BatchAggregates
{
    public enum TransferType
    {
        [Display(Name = "PIX")]
        PIX,
        [Display(Name = "TED")]
        TED
    }
}
