using Dapper;
using Npgsql;
using Scharff.Domain.Response.Company.GetSunatRegimenById;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Company.GetSunatRegimenById
{
    public class GetSunatRegimenByIdQuery : IGetSunatRegimenByIdQuery
    {
        private readonly IDbConnection _connection;

        public GetSunatRegimenByIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<ResponseGetSunatRegimenById> GetSunatRegimenById(int idCompany)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);
            try
            {

                string sql = @"SELECT 
                                      e.id, 
                                      e.razon_social company_name, 
                                      CASE COALESCE(e.regimen_sunat, false) 
                                          WHEN true THEN true
                                          ELSE false
                                      END AS sunat_regimen,
									  ecc.numero_cuenta_corriente as current_account_number 
                                FROM 
                                      		nsf.empresa e
								LEFT JOIN 	nsf.empresa_cuenta_corriente ecc on e.id = ecc.id_empresa and id_banco=1
                                WHERE e.id = @idCompany;";

                var queryArgs = new { idCompany };

                var res = await connection.QueryFirstOrDefaultAsync<ResponseGetSunatRegimenById>(sql, queryArgs);
                if (res == null) throw new ValidationException("No se encontró la Empresa");


                return res;
            }
            catch (NpgsqlException err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    }
}
