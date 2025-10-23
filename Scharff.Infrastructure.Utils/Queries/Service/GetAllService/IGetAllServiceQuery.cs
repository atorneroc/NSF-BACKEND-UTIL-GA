using Scharff.Domain.Response.Service.GetAllService;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Service.GetAllService
{
    public interface IGetAllServiceQuery
    {
        Task<List<ResponseGetAllService>> GetAllService();
    }
}
