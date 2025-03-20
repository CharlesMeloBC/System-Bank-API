using AccountBank.Domain.DTOs;
using AccountBank.Domain.Enums;
using FluentValidation;

namespace AccountBank.Domain.Validators
{
    public class IAccountBankValidator : AbstractValidator<AccountBankDto>
    {
        public IAccountBankValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(u => u.HolderName)
                .NotEmpty().WithMessage("Preencha seu nome")
                .MaximumLength(200).WithMessage("O nome do(a) titular excede o limite de caracteres");

            RuleFor(u => u.BankName)
                .NotEmpty().WithMessage("Preencha seu nome")
                .MaximumLength(200).WithMessage("O nome do Banco excede o limite de caracteres");

            RuleFor(u => u.HolderEmail)
                .NotEmpty().WithMessage("Informe seu email")
                .EmailAddress().WithMessage("Email inválido")
                .MaximumLength(200).WithMessage("O e-mail excede o limite de caracteres");

            RuleFor(u => u.HolderDocuments)
                .NotEmpty().WithMessage("Informe seu documento");

            RuleFor(u => u.Branch)
                .NotEmpty().WithMessage("Informe o número da agência")
                .Length(5).WithMessage("Número da agência inválido");

            RuleFor(u => u.HolderType)
                .NotEmpty().WithMessage("Campo não pode ser vazio")
                .IsEnumName(typeof(HolderType), caseSensitive: false)
                .WithMessage("HolderType deve ser 'NATURAL' ou 'LEGAL'");

            RuleFor(u => u.TypeAccount)
                .NotEmpty().WithMessage("Campo não pode ser vazio")
                .IsEnumName(typeof(AccountType), caseSensitive: false)
                .WithMessage("TypeAccount deve ser 'PAYMENT', 'CURRENT', 'SAVINGS' ou 'SALARY'");
        }
    }
}
