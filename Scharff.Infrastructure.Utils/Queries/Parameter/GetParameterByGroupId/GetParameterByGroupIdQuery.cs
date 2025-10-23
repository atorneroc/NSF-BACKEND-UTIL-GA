using Dapper;
using Npgsql;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.Parameter.GetParameterByGroupId;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetParameterByGroupId
{
    public class GetParameterByGroupIdQuery : IGetParameterByGroupIdQuery
    {
        private readonly IDbConnection _connection;

        public GetParameterByGroupIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetParameterByGroupId>> GetParameterByGroupId(string groupId)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT 
			                        P.id,
			                        P.descripcion AS name,
                                    P.descripcion AS description,
                                    P.codigo_detalle AS detail_code,
			                        P.tamanio_atributo AS attribute_length ,
			                        P.codigo_correlacional AS correlational_code,
			                        P.codigo_tci AS tci_code,
			                        P.codigo_sap AS sap_code,
                                    p.descripcion_corta AS short_name,
                                    p.codigo_integracion AS code,
                                    concat(TRIM(p.codigo_integracion), ' ' ,TRIM(p.descripcion_corta)) AS full_name,
                                    p.valor_minimo AS min_val,
                                    p.valor_maximo AS max_val
                        FROM    	nsf.detalle_parametro P 
                        INNER JOIN  nsf.parametro_general PG ON P.codigo_general = PG.codigo_general
                        WHERE		P.codigo_general = @groupId
                        AND         P.estado = true
                        ORDER BY    P.codigo_detalle ASC";

                    var queryArgs = new { groupId };

                    IEnumerable<ResponseGetParameterByGroupId> parameters = await connection.QueryAsync<ResponseGetParameterByGroupId>(sql, queryArgs);
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
