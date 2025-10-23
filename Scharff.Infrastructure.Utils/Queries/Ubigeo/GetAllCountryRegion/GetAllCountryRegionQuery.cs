using Dapper;
using Npgsql;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.Ubigeo.GetAllCountryRegion;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetAllCountryRegion
{
    public class GetAllCountryRegionQuery : IGetAllCountryRegionQuery
    {
        private readonly IDbConnection _connection;

        public GetAllCountryRegionQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetAllCountryRegion>> GetAllCountryRegion(string? Term)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT   id, codigo_ubigeo Ubigeo_Code, descripcion Description, codigo_sap_pais Sap_Country_Code, codigo_sap_region
	                                         Sap_Region_Code
                                    FROM     nsf.vw_ubicacion_geografica
                                    WHERE  
                                    UPPER(TRIM(descripcion)) LIKE UPPER(TRIM(@name))
                                    ORDER BY 3
                                    limit 20
                                    ";
                     
                    var queryArgs = new { name = "%" + Term + "%" };


                    IEnumerable<ResponseGetAllCountryRegion> parameters = await connection.QueryAsync<ResponseGetAllCountryRegion>(sql, queryArgs);
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
