using FluentValidation;

namespace Scharff.Application.Queries.Parameter.ValidateConfiguredService
{
    public class ValidateConfiguredServiceValidator : AbstractValidator<ValidateConfiguredServiceQuery>
    {
        public ValidateConfiguredServiceValidator()
        {
            RuleFor(x => x.companyDocNumber).NotNull().NotEmpty().WithMessage("Debe ingresar el campo empresa.");
            RuleFor(x => x.branchCode).NotNull().NotEmpty().WithMessage("Debe ingresar el campo sucursal.");
            RuleFor(x => x.businessUnitCode).NotNull().NotEmpty().WithMessage("Debe ingresar el campo unidad de negocio.");
            RuleFor(x => x.serviceTypeCode).NotNull().NotEmpty().WithMessage("Debe ingresar el campo codigo tipo de servicio.");
        }
    }
}