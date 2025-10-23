using Dapper;
using Npgsql;
using Scharff.Domain.Response.Ubigeo.GetAllCountryRegion;
using Scharff.Domain.Response.Ubigeo.GetByIdCountryRegion;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.GetByIdCountryRegion
{
    public class GetByIdCountryRegionQuery : IGetByIdCountryRegionQuery
    {
        private readonly IDbConnection _connection;

        public GetByIdCountryRegionQuery(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<ResponseGetByIdCountryRegion>> GetByIdCountryRegion(int id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT   id, codigo_ubigeo Ubigeo_Code, descripcion Description, codigo_sap_pais Sap_Country_Code, codigo_sap_region
	                                         Sap_Region_Code
                                    FROM     nsf.vw_ubicacion_geografica
                                    WHERE id = @id
                                    ORDER BY 3";
                    var queryArgs = new {  id };

                    IEnumerable<ResponseGetByIdCountryRegion> parameters = await connection.QueryAsync<ResponseGetByIdCountryRegion>(sql, queryArgs);
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
