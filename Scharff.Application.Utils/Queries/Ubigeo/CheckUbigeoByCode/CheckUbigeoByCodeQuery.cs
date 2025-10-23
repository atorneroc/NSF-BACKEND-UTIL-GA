using MediatR;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoByCode;

namespace Scharff.Application.Queries.Ubigeo.CheckUbigeoByCode
{
    public class CheckUbigeoByCodeQuery :IRequest<List<ResponseCheckUbigeoByCode>>
    {
        public string Ubigeo_code { get; set; } = string.Empty;
    }
}