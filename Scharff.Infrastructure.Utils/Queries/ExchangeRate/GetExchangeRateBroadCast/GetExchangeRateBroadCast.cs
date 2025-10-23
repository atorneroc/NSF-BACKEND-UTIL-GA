using Dapper;
using Npgsql;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateBroadCast;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateBroadCast
{
    public class GetExchangeRateBroadCast : IGetExchangeRateBroadCast
    {
        private readonly IDbConnection _connection;

        public GetExchangeRateBroadCast(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<ResponseGetExchangeRateBroadCast> GetExchangeRateByBroadCast(DateTime broadCast)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);
            try
            {

                string sql = @"SELECT  
                                          tc.id,
                                          tc.fecha AS date_change,
                                          tc.venta AS certificate_purchase,
                                          tc.venta AS certificate_sale,
                                          tc.venta AS bank_purchase,
                                          tc.venta AS bank_sale,
                                          tc.venta AS parallel_purchase,
                                          tc.venta AS parallel_sale,
                                          tc.fecha_creacion AS creation_date,
                                          tc.usuario_creacion AS creation_author,
                                          tc.fecha_modificacion AS modification_date,
                                          tc.usuario_modificacion AS modification_author
                                          FROM  nsf.tipo_cambio tc
                                          WHERE tc.fecha = @broadCast;
                                          ";

                var queryArgs = new { broadCast };

                var exchangeRate = await connection.QueryFirstOrDefaultAsync<ResponseGetExchangeRateBroadCast>(sql, queryArgs);


                return exchangeRate;
            }
            catch (NpgsqlException err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    }
}
