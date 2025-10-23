using MediatR;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByCode;

namespace Scharff.Application.Queries.Ubigeo.GetUbigeoByCode
{
    public class GetUbigeoByCodeQuery : IRequest<List<ResponseGetUbigeoByCode>>
    {
        public List<string> ubigeoCode { get; set; } = new List<string>();
    }
}
