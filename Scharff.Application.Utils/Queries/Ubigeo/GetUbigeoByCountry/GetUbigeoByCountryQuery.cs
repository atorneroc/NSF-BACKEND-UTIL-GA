using MediatR;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByContry;

namespace Scharff.Application.Queries.Ubigeo.GetUbigeoByCountry
{
    public class GetUbigeoByCountryQuery : IRequest<List<ResponseGetUbigeoByCountry>>
    {
        public string term { get; set; } = string.Empty;
        public int size { get; set; }
    }
}
