using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.GetStoreByBusinessUnitId;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetStoreByBusinessUnitId
{
    public class GetStoreByBusinessUnitIdQuery : IGetStoreByBusinessUnitIdQuery
    {
        private readonly IDbConnection _connection;

        public GetStoreByBusinessUnitIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetStoreByBusinessUnitId>> GetStoreByBusinessUnitId(int CompanyId, int branchOfficeId, int businessUnitId)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @"SELECT distinct 
                                            a.id_producto AS id, 
                                            a.descripcion AS name,
                                            eob.id id_estructura_base,
                                            a.codigo_integracion_of AS integration_code
                                   FROM   nsf.estructura_organizacional_base eob                        LEFT JOIN
  	                                      nsf.producto a ON eob.id_producto = a.id_producto                LEFT JOIN
	                                      nsf.sucursal s ON eob.id_sucursal = s.id                      LEFT JOIN
                                          nsf.unidad_negocio un ON eob.id_unidad_negocio = un.id
                                   WHERE  eob.id_empresa = @CompanyId
                                     AND  eob.id_sucursal = @branchOfficeId
                                     AND  eob.id_unidad_negocio = @businessUnitId
                                     AND a.estado = true";

                    var queryArgs = new { CompanyId, branchOfficeId, businessUnitId };

                    IEnumerable<ResponseGetStoreByBusinessUnitId> result = await connection.QueryAsync<ResponseGetStoreByBusinessUnitId>(sql, queryArgs);
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
