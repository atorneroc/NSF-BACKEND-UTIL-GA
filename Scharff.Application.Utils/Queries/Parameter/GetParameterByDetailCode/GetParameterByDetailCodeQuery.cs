using MediatR;
using Scharff.Domain.Response.Parameter.GetParameterByCodeDetail;

namespace Scharff.Application.Queries.Parameter.GetParameterByDetailCode
{
    public class GetParameterByDetailCodeQuery : IRequest<List<ResponseGetParameterByDetailCode>>
    {
        public List<string> Lst_Codes { get; set; }
    }
}