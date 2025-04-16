namespace Transactions.Core.Domain.Aggregates.BatchAggregates
{
    public interface IBatchTranferItem
    {
        Task AddAsync(BatchTransfers transfer);
    }
}
