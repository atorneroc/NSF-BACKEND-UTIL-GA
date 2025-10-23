using MediatR;
using Scharff.Domain.Response.Parameter.ValidateExternalBaseStructure;

namespace Scharff.Application.Queries.Parameter.ValidateExternalBaseStructure
{
    public class ValidateExternalBaseStructureQuery : IRequest<BaseServiceResponse>
    {
        public string DocumentNumber { get; set; }
        public string BranchCodeIntOF { get; set; }
        public string BusinessUnitCodeIntOF { get; set; }
        public string ProductCodeIntOF { get; set; }
        public string? ServiceCodeIntOF { get; set; }

    }
}
