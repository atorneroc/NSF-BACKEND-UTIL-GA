using MediatR;
using Scharff.Domain.Response.Ubigeo.GetAllCountryRegion;

namespace Scharff.Application.Queries.Parameter.GetAllCountryRegion
{
    public class GetAllCountryRegionQuery : IRequest<List<ResponseGetAllCountryRegion>>{
        public string? Term { get; set; } = string.Empty;

    }
}
