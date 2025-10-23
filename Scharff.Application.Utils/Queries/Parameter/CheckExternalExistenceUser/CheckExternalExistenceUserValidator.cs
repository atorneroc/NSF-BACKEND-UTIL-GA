using FluentValidation;

namespace Scharff.Application.Queries.Parameter.CheckExternalExistenceUser
{
    public class CheckExternalExistenceUserValidator : AbstractValidator<CheckExternalExistenceUserQuery>
    {
        public CheckExternalExistenceUserValidator()
        {
            RuleFor(x => x.User_Email)
            .NotNull().WithMessage("El email no puede ser nulo.")
            .NotEmpty().WithMessage("El email no puede estar vacío.")
            .EmailAddress().WithMessage("El email debe tener un formato válido.");
        }
    }
}
