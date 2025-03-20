using AccountBank.Domain.Models;
using FluentValidation;

namespace AccountBank.Domain.Validators
{
    public class BalanceValidator : AbstractValidator<BalanceModel>
    {
        public BalanceValidator() 
        {
            RuleFor(b => b.AvaliableAmount)
                .NotEmpty()
                .PrecisionScale(18, 2, false)
                .WithMessage("Qualquer valor monetário deve possuir no máximo 18 dígitos ");
            RuleFor(b=> b.BlokedAmount).NotEmpty();
        }
    }
}
