using Transactions.Domain.DTOs;
using FluentValidation;

namespace Transactions.Domain.Validators
{
    public class ITrasationValidator : AbstractValidator<TransactionDto>
    {
        public ITrasationValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("Informe o Id da transação");

            RuleFor(t => t.TransactionType)
                .NotEmpty().WithMessage("Informe o tipo da transação");

            RuleFor(t => t.Amount)
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero");

            RuleFor(t => t.BankAccountId)
                .NotEmpty().WithMessage("Informe o Id da conta");

            RuleFor(t => t.CounterpartyBankCode)
                .NotEmpty().WithMessage("Contraparte: Código do banco")
                .Length(3).WithMessage("Contraparte: O código do banco deve ter exatamente 3 caracteres");

            RuleFor(t => t.CounterpartyBankName)
                .NotEmpty().WithMessage("Contraparte: Nome do banco")
                .MaximumLength(100).WithMessage("Contraparte: O nome do banco excede o limite de 100 caracteres");

            RuleFor(t => t.CounterpartyBranch)
                .NotEmpty().WithMessage("Contraparte: Nome da agência")
                .Length(5).WithMessage("Contraparte: O número da agência deve ter exatamente 5 caracteres");

            RuleFor(t => t.CounterpartyAccountNumber)
                .NotEmpty().WithMessage("Contraparte: Número da conta")
                .MaximumLength(20).WithMessage("Contraparte: O número da conta não pode exceder 20 caracteres");

            RuleFor(t => t.CounterpartyAccountType)
                .NotEmpty().WithMessage("Contraparte: Tipo da conta");

            RuleFor(t => t.CounterpartyHolderName)
                .NotEmpty().WithMessage("Contraparte: Nome do titular")
                .MaximumLength(200).WithMessage("Contraparte: O nome do titular excede o limite de 200 caracteres");

            RuleFor(t => t.CounterpartyHolderType)
                .NotEmpty().WithMessage("Contraparte: Tipo do titular (CPF/CNPJ)");

            RuleFor(t => t.CounterpartyHolderDocument)
                .NotEmpty().WithMessage("Contraparte: Documento do titular")
                .Must(doc => doc.Length == 11 || doc.Length == 14)
                .WithMessage("Contraparte: Documento inválido. CPF deve ter 11 dígitos e CNPJ 14 dígitos");
        }
    }
}
