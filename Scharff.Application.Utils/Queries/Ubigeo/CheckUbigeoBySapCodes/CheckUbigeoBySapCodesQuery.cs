using MediatR;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoBySapCodes;

namespace Scharff.Application.Queries.Ubigeo.CheckUbigeoBySapCodes
{
    public class CheckUbigeoBySapCodesQuery : IRequest<List<ResponseUbigeoLocationBySapCodes>>
    {
        public string SapCountryCode { get; set; } = string.Empty;
        public string SapRegionCode { get; set; } = string.Empty;
    }
}
