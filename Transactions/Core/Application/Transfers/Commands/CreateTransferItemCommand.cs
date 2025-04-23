using Transactions.Core.Aplication.Transfers.DTOs;

namespace Transactions.Core.Aplication.Transfers.Commands
{
    public class CreateTransferCommand
    {
        public CreateTransferItemDto Dto { get; }

        public CreateTransferCommand(CreateTransferItemDto dto)
        {
            Dto = dto;
        }
    }

}
