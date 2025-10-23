using Dapper;
using Npgsql;
using Scharff.Domain.Response.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using Scharff.Domain.Response.UbicacionGeografica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.UbicacionGeografica.GetUbicacionGeograficaById
{
    public class GetUbicacionGeograficaByIdQuery : IGetUbicacionGeograficaByIdQuery
    {
        private readonly IDbConnection _connection;

        public GetUbicacionGeograficaByIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetUbicacionGeograficaById>> GetUbicacionGeograficaById(int id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @"
                                   WITH RECURSIVE ubi AS (
                                    SELECT ug.id,
                                        1 AS level,
                                        ARRAY[ug.descripcion::character varying] AS descripciones,
                                        ARRAY[ug.codigo_sap::character varying] AS codigos_sap
                                    FROM nsf.ubicacion_geografica ug
                                    WHERE ug.id_padre IS NULL
                                    UNION ALL
                                         SELECT 
			                                c.id,
                                            p.level + 1,
                                            p.descripciones || c.descripcion,
                                            p.codigos_sap || c.codigo_sap
                                           FROM nsf.ubicacion_geografica c
                                             JOIN ubi p ON c.id_padre = p.id)
                                    SELECT 
 	                                    ubi.id,
                                        array_to_string(ubi.descripciones, ', '::text) AS pais,
                                        split_part(array_to_string(ubi.codigos_sap, ', '::text), ', '::text, 1) AS codigo_pais    
                                   FROM ubi
                                   WHERE ubi.level = 3 and ubi.id = @id" ;

                    var queryArgs = new { id };
                    IEnumerable<ResponseGetUbicacionGeograficaById> parameters = await connection.QueryAsync<ResponseGetUbicacionGeograficaById>(sql, queryArgs);
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
