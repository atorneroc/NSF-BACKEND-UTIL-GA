using MediatR;
using Scharff.Domain.Response.Parameter.GetIgv;

namespace Scharff.Application.Queries.Parameter.GetIgv
{
    public class GetIgvQuery : IRequest<List<ResponseGetIgv>> { }
}
