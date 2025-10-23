using Dapper;
using Npgsql;
using Scharff.Domain.Response.ExchangeRate.GetAllExchangeRate;
using Scharff.Domain.Response.ExchangeRate.GetExchangeRateById;
using Scharff.Domain.Response.Parameter.GetIgv;
using Scharff.Domain.Response.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.ExchangeRate.GetExchangeRateById
{
    public class GetExchangeRateByIdQuery : IGetExchangeRateByIdQuery
    {
        private readonly IDbConnection _connection;

        public GetExchangeRateByIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ResponseGetExchangeRateById> GetExchangeRateById(int id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT 
                                        id AS id,
                                        fecha AS date_change,
                                        compra AS bank_purchase,
                                        venta AS bank_sale
                                    FROM
                                         nsf.tipo_cambio 
                                    WHERE id = @id;
                                  ";

                    var queryArgs = new { id };

                    var result = await connection.QueryFirstOrDefaultAsync<ResponseGetExchangeRateById>(sql,queryArgs);


                    return result;
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
