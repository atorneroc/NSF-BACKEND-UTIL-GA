using FluentValidation;

namespace Scharff.Application.Commands.ExchangeRate.UpdateExchangeRate
{
    public class UpdateExchangeRateCommandValidator : AbstractValidator<UpdateExchangeRateCommand>
    {
        public UpdateExchangeRateCommandValidator()
        {
            RuleFor(client => client.bank_purchase)
                .NotEmpty()
                .WithMessage("Por favor ingresar el tipo de cambio compra");
            RuleFor(client => client.bank_sale)
               .NotEmpty()
               .WithMessage("Por favor ingresar el tipo de cambio venta");
        }
    }
}
