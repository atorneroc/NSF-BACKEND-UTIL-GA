using Dapper;
using Npgsql;
using Scharff.Domain.Response.BillingCourt;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.BillingCourt.GetBusinessUnitByIdCompany
{
    public class GetBusinessUnitByIdCompanyQuery : IGetBusinessUnitByIdCompanyQuery
    {

        private readonly IDbConnection _connection;

        public GetBusinessUnitByIdCompanyQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetBusinessUnitByIdCompany>> GetBusinessUnitByIdCompany(int id_company)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @"
                                      SELECT DISTINCT 
				                                          UNE.id id_unidad_negocio, 
				                                          UNE.descripcion descripcion_unidad_negocio
                                                 FROM     nsf.estructura_organizacional_base EOB 				  				   INNER JOIN
	                                                      nsf.empresa 						 EMP ON EMP.id = EOB.id_empresa 	   INNER JOIN
	                                                      nsf.sucursal 						 SUC ON SUC.id = EOB.id_sucursal 	   INNER JOIN
	                                                      nsf.unidad_negocio 				 UNE ON UNE.id = EOB.id_unidad_negocio 
                                                 WHERE    EMP.id = @id_company
                                                 ORDER BY 2";                                        

                    var queryArgs = new { id_company };
                    IEnumerable<ResponseGetBusinessUnitByIdCompany> parameters = await connection.QueryAsync<ResponseGetBusinessUnitByIdCompany>(sql, queryArgs);                    
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
