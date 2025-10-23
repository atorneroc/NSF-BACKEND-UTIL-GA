using MediatR;
using Scharff.Domain.Response.Parameter.GetAllCompanies;

namespace Scharff.Application.Queries.Parameter.GetAllCompanies
{
    public class GetAllCompaniesQuery : IRequest<List<ResponseGetAllCompanies>> { }

}
