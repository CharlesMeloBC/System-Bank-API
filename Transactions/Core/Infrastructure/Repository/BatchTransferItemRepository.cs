using Transactions.Core.Domain.Aggregates.BatchAggregates;
using Microsoft.EntityFrameworkCore;
using Transactions.Core.Infrastructure.Data;

namespace Transactions.Infrastructure.Repositories
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
            await _context.BatchTransferItems.AddAsync(transfer);
            await _context.SaveChangesAsync();
        }
    }
}
