using Dapper;
using Npgsql;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoBySapCodes;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoBySapCodes
{
    public class CheckUbigeoBySapCodesQuery : ICheckUbigeoBySapCodesQuery
    {
        private readonly IDbConnection _dbConnection;
        public CheckUbigeoBySapCodesQuery(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<List<ResponseUbigeoLocationBySapCodes>> CheckUbigeoBySapCodes(string sapCountryCode, string sapRegionCode)
        {
            using IDbConnection connection = new NpgsqlConnection(_dbConnection.ConnectionString);
            try
            {
                string sql = @" SELECT 
                                    id AS Id, 
                                    codigo_ubigeo AS UbigeoCode, 
                                    descripcion AS Description, 
                                    codigo_sap_pais AS SapCountryCode, 
                                    codigo_sap_region AS SapRegionCode 
                                    FROM nsf.vw_ubicacion_geografica 
                                WHERE codigo_sap_pais = @sapCountryCode 
                                AND codigo_sap_region = @sapRegionCode
                                LIMIT 1";

                var queryArgs = new
                {
                    sapCountryCode,
                    sapRegionCode
                };

                IEnumerable<ResponseUbigeoLocationBySapCodes> parameters = await connection.QueryAsync<ResponseUbigeoLocationBySapCodes>(sql, queryArgs);
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
