using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.GetIgv;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetIgv
{
    public class GetIgvQuery : IGetIgvQuery
    {
        private readonly IDbConnection _connection;

        public GetIgvQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetIgv>> GetIgv()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT 
                                        mes_inicio AS start_month,
                                        anio_inicio AS start_year, 
                                        valor_actual AS current_value 
                                    FROM nsf.igv
                                    WHERE mes_inicio<=lpad(extract(month from now())::text, 2, '0')
                                    AND  anio_inicio<=extract(year from now())::text
                                    ORDER BY mes_inicio DESC,anio_inicio DESC
                                    LIMIT 1";

                    IEnumerable<ResponseGetIgv> parameters = await connection.QueryAsync<ResponseGetIgv>(sql);
                    return parameters.ToList();
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
