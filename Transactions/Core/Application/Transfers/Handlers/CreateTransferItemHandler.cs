using Transactions.Core.Aplication.Transfers.Commands;
using Transactions.Core.Domain.Aggregates.BatchAggregates;

namespace Transactions.Core.Application.Transfers.Handlers
{
    public class CreateTransferHandler
    {
        private readonly IBatchTranferItem _repository;

        public CreateTransferHandler(IBatchTranferItem repository)
        {
            _repository = repository;
        }

        public async Task<BatchTransfersItem> HandleAsync(CreateTransferCommand command)
        {
            var dto = command.Dto;

            var transfer = new BatchTransfersItem(
                dto.Amount,
                dto.BeneficiaryAccountNumber,
                dto.TransferType
            );

            await _repository.AddAsync(transfer);

            return transfer;
        }
    }
}
