using Transactions.Core.Domain.Aggregates.BatchAggregates;
using Transactions.Core.Infrastructure.Data;

namespace Transactions.Core.Infrastructure.Repository
{
    public class BatchTransferItemRepository : IBatchTranferItem
    {
        private readonly AppDbContext _context;

        public BatchTransferItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BatchTransfersItem transfer)
        {
            var transferItem = new BatchTransfersItem(
                transfer.Amount,
                transfer.BeneficiaryAccountNumber,
                transfer.TransferType
            );

            await _context.BatchTransferItems.AddAsync(transferItem);
            await _context.SaveChangesAsync();
        }
    }
}
