using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.GetParameterByCodeDetail;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByDetailCode
{
    public class GetParameterByDetailCodeQuery : IGetParameterByDetailCodeQuery
    {
        private readonly IDbConnection _connection;
        public GetParameterByDetailCodeQuery(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }
        public async Task<List<ResponseGetParameterByDetailCode>> GetParameterByCodeDetail(List<string> lstCodes)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);
            if (lstCodes == null || !lstCodes.Any())
            {
                return new List<ResponseGetParameterByDetailCode>();
            }
            try
            {
                var idParameters = string.Join(", ", lstCodes.Select((id, index) => $"@id{index}"));

                string sql = $@"
                            SELECT 
                                P.id,
                                P.descripcion AS name,
                                P.descripcion AS description,
                                P.codigo_detalle AS detail_code,
                                P.codigo_general AS general_code,
                                PG.descripcion AS general_description
                            FROM nsf.detalle_parametro P 
                            INNER JOIN nsf.parametro_general PG ON P.codigo_general = PG.codigo_general
                            WHERE P.estado = true 
                            AND CONCAT(P.codigo_detalle, '-', P.codigo_general) = ANY(@lstCodes)
                            ORDER BY P.id ASC";

                var parameters = new { lstCodes };

                var parametersList = await connection.QueryAsync<ResponseGetParameterByDetailCode>(sql, parameters);
                return parametersList.ToList();
            }
            catch (NpgsqlException err)
            {
                throw new Exception("Error al recuperar los parámetros", err);
            }
        }
    }
}