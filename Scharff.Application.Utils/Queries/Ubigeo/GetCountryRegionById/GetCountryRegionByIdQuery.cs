using MediatR;
using Scharff.Domain.Response.Ubigeo.GetByIdCountryRegion;

namespace Scharff.Application.Queries.Ubigeo.GetCountryRegionById
{
    public class GetCountryRegionByIdQuery : IRequest<List<ResponseGetByIdCountryRegion>>
    {
        public int id { get; set; }

    }
}
