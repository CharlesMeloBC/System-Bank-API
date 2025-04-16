using FluentValidation;
using Transactions.Core.Aplication.Transfers.DTOs;


namespace Transactions.Core.Application.Transfers.Validators
{

    public class CreateTransferDtoValidator : AbstractValidator<CreateTransferDto>
    {
        public CreateTransferDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("O valor da transferência deve ser maior que zero.");

            RuleFor(x => x.BankAccountNumber)
                .NotEmpty().WithMessage("O número da conta bancária é obrigatório.")
                .Length(8, 12).WithMessage("O número da conta deve ter entre 8 e 12 dígitos.");

            RuleFor(x => x.TransferType)
                .IsInEnum().WithMessage("Tipo de transferência inválido.");
        }
    }

}
