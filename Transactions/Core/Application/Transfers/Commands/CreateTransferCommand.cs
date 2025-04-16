using Transactions.Core.Aplication.Transfers.DTOs;

namespace Transactions.Core.Aplication.Transfers.Commands
{
    public class CreateTransferCommand
    {
        public CreateTransferDto Dto { get; }

        public CreateTransferCommand(CreateTransferDto dto)
        {
            Dto = dto;
        }
    }

}
