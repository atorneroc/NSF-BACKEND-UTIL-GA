using AutoMapper;
using MediatR;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.Parameter.ValidateExternalBaseStructure;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateExternalBaseStructure;

namespace Scharff.Application.Queries.Parameter.ValidateExternalBaseStructure
{
    public class ValidateExternalBaseStructureHandler : IRequestHandler<ValidateExternalBaseStructureQuery, BaseServiceResponse>
    {
        private readonly IValidateExternalBaseStructureQuery _validateExternalBaseStructureQuery;
        private readonly IMapper _mapper;

        public ValidateExternalBaseStructureHandler(IValidateExternalBaseStructureQuery validateExternalBaseStructureQuery, IMapper mapper)
        {
            _validateExternalBaseStructureQuery = validateExternalBaseStructureQuery;
            _mapper = mapper;

        }
        public async Task<BaseServiceResponse> Handle(ValidateExternalBaseStructureQuery request, CancellationToken cancellationToken)
        {
            var externalBaseStructure = _mapper.Map<ExternalBaseStructure>(request);

            var result = await _validateExternalBaseStructureQuery.ValidateExternalBaseStructure(externalBaseStructure);

            return result;
        }
    }
}
