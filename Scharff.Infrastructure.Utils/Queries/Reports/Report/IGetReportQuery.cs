namespace Scharff.Infrastructure.PostgreSQL.Queries.Reports.Report
{
    public interface IGetReportQuery
    {
        Task<List<ResponseAllLiquidation>> GetReport();
    }
}
