using FluentValidation;

namespace Scharff.Application.Commands.ExchangeRate.RegisterExchangeRate
{
    public class RegisterExchangeRateCommandValidator : AbstractValidator<RegisterExchangeRateCommand>
    {
        public RegisterExchangeRateCommandValidator() {
            RuleFor(client => client.change_date)
                .NotEmpty()
                .WithMessage("Por favor ingresar la fecha de tipo de cambio");
        }
    }
}
