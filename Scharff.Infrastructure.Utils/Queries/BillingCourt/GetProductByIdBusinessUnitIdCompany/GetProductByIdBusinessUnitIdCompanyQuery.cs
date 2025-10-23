using Dapper;
using Npgsql;
using Scharff.Domain.Response.BillingCourt;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetProductByIdBusinessUnitIdCompany
{
    public class GetProductByIdBusinessUnitIdCompanyQuery : IGetProductByIdBusinessUnitIdCompanyQuery
    {

        private readonly IDbConnection _connection;

        public GetProductByIdBusinessUnitIdCompanyQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetProductByIdBusinessUnitIdCompany>> GetProductByIdBusinessUnitIdCompany(int id_company, int id_business_unit)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @"
                                      SELECT DISTINCT 				
				                                        EOB.id_producto, 
				                                        PRO.descripcion descripcion_producto
                                        FROM     nsf.estructura_organizacional_base EOB 				  				   INNER JOIN
	                                             nsf.empresa 						 EMP ON EMP.id = EOB.id_empresa 	   INNER JOIN
	                                             nsf.sucursal 						 SUC ON SUC.id = EOB.id_sucursal 	   INNER JOIN
	                                             nsf.unidad_negocio 				 UNE ON UNE.id = EOB.id_unidad_negocio INNER JOIN
	                                             nsf.producto 						 PRO ON EOB.id_producto = PRO.id_producto
                                        WHERE    EMP.id = @id_company
                                          AND    UNE.id = @id_business_unit
                                        ORDER BY 2";                                        

                    var queryArgs = new { id_company, id_business_unit };
                    IEnumerable<ResponseGetProductByIdBusinessUnitIdCompany> parameters = await connection.QueryAsync<ResponseGetProductByIdBusinessUnitIdCompany>(sql, queryArgs);                    
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
