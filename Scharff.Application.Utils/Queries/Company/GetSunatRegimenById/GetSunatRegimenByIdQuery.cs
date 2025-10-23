using MediatR;
using Scharff.Domain.Response.Company.GetSunatRegimenById;

namespace Scharff.Application.Queries.Company.GetSunatRegimenById
{
    public class GetSunatRegimenByIdQuery : IRequest<ResponseGetSunatRegimenById>
    {
        public int Id_Company { get; set; }
    }
}
