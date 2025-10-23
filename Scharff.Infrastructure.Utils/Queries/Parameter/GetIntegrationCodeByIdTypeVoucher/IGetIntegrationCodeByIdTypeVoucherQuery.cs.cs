using Scharff.Domain.Response.Parameter.GetIntegrationCodeByIdTypeVoucher;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetIntegrationCodeByIdTypeVoucher
{
    public interface IGetIntegrationCodeByIdTypeVoucherQuery
    {
        Task<List<ResponseGetIntegrationCodeByIdTypeVoucher>> GetIntegrationCodeByIdTypeVoucher(int idTypeVoucher);
    }
}
