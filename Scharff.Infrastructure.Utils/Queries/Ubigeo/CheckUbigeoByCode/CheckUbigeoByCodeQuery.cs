using Dapper;
using Npgsql;
using Scharff.Domain.Response.Ubigeo.CheckUbigeoByCode;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Ubigeo.CheckUbigeoByCode
{
    public class CheckUbigeoByCodeQuery : ICheckUbigeoByCodeQuery
    {
        private readonly IDbConnection _dbConnection;
        public CheckUbigeoByCodeQuery(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<List<ResponseCheckUbigeoByCode>> CheckUbigeoByCode(string ubigeoCode)
        {
            using (IDbConnection connection = new NpgsqlConnection(_dbConnection.ConnectionString))
                try
                {
                    string sql = @" WITH RECURSIVE ubicacion_hierarchy AS (
                                -- Comienza con el nodo que tiene el código de ubigeo dado
                                SELECT id, id_padre, descripcion, codigo_ubigeo, 0 AS nivel
                                FROM nsf.ubicacion_geografica
                                WHERE codigo_ubigeo = @ubigeoCode
  
                                UNION ALL
                                -- Unirse recursivamente con los nodos padres
                                SELECT ug.id, ug.id_padre, ug.descripcion, ug.codigo_ubigeo, uh.nivel + 1
                                FROM nsf.ubicacion_geografica ug
                                INNER JOIN ubicacion_hierarchy uh ON ug.id = uh.id_padre
                                ),
                                max_nivel AS (
                                -- Calcular el nivel máximo alcanzado
                                SELECT MAX(nivel) AS max_nivel FROM ubicacion_hierarchy
                                )
                                SELECT uh.id AS Id, 
                                       uh.id_padre AS Parent_id,
                                       uh.descripcion AS Description,
                                       uh.codigo_ubigeo AS UbigeoCode,
                                       max_nivel.max_nivel - uh.nivel + 1 AS Level,
                                       CASE max_nivel.max_nivel - uh.nivel + 1
                                       WHEN 1 THEN 'PAIS'
                                       WHEN 2 THEN 'DEPARTAMENTO'
                                       WHEN 3 THEN 'PROVINCIA'
                                       WHEN 4 THEN 'DISTRITO'
                                       END AS Label
                                FROM ubicacion_hierarchy uh
                                CROSS JOIN max_nivel
                                ORDER BY Level;";

                    var queryArgs = new { ubigeoCode };

                    IEnumerable<ResponseCheckUbigeoByCode> parameters = await connection.QueryAsync<ResponseCheckUbigeoByCode>(sql, queryArgs);
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