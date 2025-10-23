using Dapper;
using Npgsql;
using Scharff.Domain.Response.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio
{
    public class GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery : IGetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery
    {

        private readonly IDbConnection _connection;

        public GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio>> GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio(int id_empresa,int id_sucursal, int id_unidad_negocio)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @"SELECT pro.id , pro.descripcion, eob.id as id_estructura_organizacional_base
                    FROM   nsf.estructura_organizacional_base eob
                    INNER JOIN nsf.producto pro ON eob.id_producto = pro.id
                    WHERE eob.id_empresa = @id_empresa
                    AND eob.id_sucursal = @id_sucursal
                    and eob.id_unidad_negocio = @id_unidad_negocio";                                        

                    var queryArgs = new { id_empresa, id_sucursal, id_unidad_negocio };
                    IEnumerable<ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio> parameters = await connection.QueryAsync<ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio>(sql, queryArgs);                    
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
