using Dapper;
using Npgsql;
using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.Utils;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetAllExchangeRate
{
    public class GetAllExchangeRateQuery : IGetAllExchangeRateQuery
    {
        private readonly IDbConnection _connection;

        public GetAllExchangeRateQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<PaginatedResponse<ResponseGetAllExchangeRate>> GetAllExchangeRate(int pageNumber, int pageSize,DateTime? date_change)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    pageSize = (pageSize == 0) ? 10 : pageSize;

                    int skip = (pageNumber) * pageSize;
                    int take = pageSize;
                    string sql = $@" SELECT 
                                        COUNT(1) 
                                     FROM 
                                         nsf.tipo_cambio
                                     WHERE  
                                         (COALESCE(@date_change,null) IS NULL OR
										 (fecha::date = @date_change::date ) ); ";
                    sql += @" SELECT 
                                        id AS id,
                                        fecha AS date_change,
                                        compra AS bank_purchase,
                                        venta AS bank_sale,
                                        CASE 
										WHEN fecha::date >= current_date::date
										THEN true
										ELSE false 
										END edit
                                    FROM
                                         nsf.tipo_cambio 
                                    WHERE 
                                         (COALESCE(@date_change,null) IS NULL OR
										 (fecha::date = @date_change::date ) )
                                    ORDER BY fecha desc
                                    OFFSET 
                                        @skip ROWS 
                                    FETCH NEXT 
                                        @take ROWS ONLY;
                                  ";

                    var queryArgs = new { skip, take, date_change };

                    PaginatedResponse<ResponseGetAllExchangeRate> newPaginate = new PaginatedResponse<ResponseGetAllExchangeRate>();
                    var orders = await connection.QueryMultipleAsync(sql, queryArgs);

                    newPaginate.Total_rows = orders.ReadFirst<int>();
                    newPaginate.Result = orders.Read<ResponseGetAllExchangeRate>().ToList();

                    return newPaginate;
                }
                catch (NpgsqlException err)
                {
                    Console.WriteLine(err);
                    throw;
                }
            }
        }
    }
}
