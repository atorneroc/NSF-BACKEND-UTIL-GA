using FluentValidation;
using Scharff.Domain.Entities;

namespace Scharff.Application.Queries.Parameter.ValidateExternalBaseStructure
{

    public class ExternalBaseStructureValidator : AbstractValidator<ValidateExternalBaseStructureQuery>
    {
        public ExternalBaseStructureValidator()
        {
            RuleFor(x => x.DocumentNumber)
           .NotEmpty().NotNull().WithMessage("El número de documento no puede estar vacío.")
           .Length(11).WithMessage("El número de documento debe tener exactamente 11 caracteres.") 
           .Matches("^[0-9]+$").WithMessage("El número de documento debe contener solo dígitos."); 

            RuleFor(x => x.ProductCodeIntOF).NotNull().NotEmpty().WithMessage("el producto no puede estar vacio.");
            RuleFor(x => x.BranchCodeIntOF).NotNull().NotEmpty().WithMessage("la sucursual no puede estar vacio.");
            RuleFor(x => x.BranchCodeIntOF).NotNull().NotEmpty().WithMessage("la unidad de negocio no puede estar vacio.");
            //RuleFor(x => x.ServiceCodeIntOF).NotNull().NotEmpty().WithMessage("el servicio no puede estar vacio.");
        }
    }
}
