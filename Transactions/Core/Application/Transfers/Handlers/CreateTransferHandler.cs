using Transactions.Core.Aplication.Transfers.Commands;
using Transactions.Core.Domain.Aggregates.BatchAggregates;

namespace Transactions.Core.Aplication.Transfers.Handlers
{
    public class CreateTransferHandler
    {
        private readonly IBatchTranferItem _repository;

        public CreateTransferHandler(IBatchTranferItem repository)
        {
            _repository = repository;
        }

        public async Task<BatchTransfers> HandleAsync(CreateTransferCommand command)
        {
            var dto = command.Dto;

            var transfer = new BatchTransfers(
                dto.Amount,
                dto.BankAccountNumber,
                dto.TransferType
            );

            await _repository.AddAsync(transfer);

            return transfer;
        }
    }
}
