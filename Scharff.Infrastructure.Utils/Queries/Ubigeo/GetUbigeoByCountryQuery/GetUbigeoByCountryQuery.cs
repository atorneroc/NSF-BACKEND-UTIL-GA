using Dapper;
using Npgsql;
using Scharff.Domain.Response.Ubigeo.GetUbigeoByContry;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetUbigeoByCountryQuery
{
    public class GetUbigeoByCountryQuery : IGetUbigeoByCountryQuery
    {
        private readonly IDbConnection _connection;
        public GetUbigeoByCountryQuery(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<ResponseGetUbigeoByCountry>> GetUbigeoByCountry(string? term, int size)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);
            try
            {
                int hasSize = size == 0 ? 10 : size;
                string sql = @" SELECT
                                tubigeo.id id, 
                                tubigeo.codigo_ubigeo ubigeo_Code, 
                                tubigeo.descripcion description,  
                                tubigeo.codigo_pais country_Code
                                FROM
                                (SELECT 
                                id, 
                                codigo_ubigeo,
                                descripcion,
                                codigo_pais,
                                ROW_NUMBER () OVER (ORDER BY id) as order
                                FROM 
                                nsf.vw_ubigeo
                                WHERE 
                                UPPER(TRIM(descripcion)) LIKE UPPER(TRIM(@name)) AND CODIGO_UBIGEO IS NOT NULL
                                )tubigeo
                                WHERE 
                                tubigeo.order <= @hasSize;";

                var queryArgs = new { name = "%" + term + "%", hasSize };

                IEnumerable<ResponseGetUbigeoByCountry> parameters = await connection.QueryAsync<ResponseGetUbigeoByCountry>(sql, queryArgs);
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