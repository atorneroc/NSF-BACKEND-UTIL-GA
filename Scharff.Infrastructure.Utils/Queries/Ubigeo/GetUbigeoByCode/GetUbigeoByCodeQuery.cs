using Dapper;
using Npgsql;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByCode;
using System.Data;
using System.Text.RegularExpressions;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCodeQuery
{
    public class GetUbigeoByCodeQuery : IGetUbigeoByCodeQuery
    {
        private readonly IDbConnection _connection;

        public GetUbigeoByCodeQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetUbigeoByCode>> GetUbigeoByCode(List<string> ubigeoCode)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT   id, codigo_ubigeo Ubigeo_Code, descripcion Description, codigo_postal Postal_Code
                                    FROM     nsf.vw_ubigeo
                                    WHERE    SUBSTRING (codigo_ubigeo, 1, 2) = ANY(@ubigeoCode)
                                    ORDER BY 3";

                    var queryArgs = new { ubigeoCode };

                    IEnumerable<ResponseGetUbigeoByCode> parameters = await connection.QueryAsync<ResponseGetUbigeoByCode>(sql, queryArgs);
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
