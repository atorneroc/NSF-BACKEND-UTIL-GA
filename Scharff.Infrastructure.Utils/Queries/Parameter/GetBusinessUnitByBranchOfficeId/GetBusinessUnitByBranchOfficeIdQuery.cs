using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.GetBusinessUnitByBranchOfficeId;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetBusinessUnitByBranchOfficeId
{
    public class GetBusinessUnitByBranchOfficeIdQuery : IGetBusinessUnitByBranchOfficeIdQuery
    {
        private readonly IDbConnection _connection;

        public GetBusinessUnitByBranchOfficeIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetBusinessUnitByBranchOfficeId>> GetBusinessUnitByBranchOfficeId(int CompanyId, int branchOfficeId)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT distinct un.id, 
                                                    un.descripcion AS name,
                                                    un.codigo_integracion_of AS integration_code
                                    FROM   nsf.estructura_organizacional_base eob LEFT JOIN
	                                       nsf.sucursal s ON eob.id_sucursal = s.id LEFT JOIN
                                           nsf.unidad_negocio un ON eob.id_unidad_negocio = un.id
                                    WHERE  eob.id_empresa  = @CompanyId 
                                      AND  eob.id_sucursal = @branchOfficeId
                                      AND un.estado = true";

                    var queryArgs = new { CompanyId, branchOfficeId };

                    IEnumerable<ResponseGetBusinessUnitByBranchOfficeId> result = await connection.QueryAsync<ResponseGetBusinessUnitByBranchOfficeId>(sql, queryArgs);
                    return result.ToList();
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
