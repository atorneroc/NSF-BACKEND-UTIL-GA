using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.GetAllCompanies;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetAllCompanies
{
    public class GetAllCompaniesQuery : IGetAllCompaniesQuery
    {
        private readonly IDbConnection _connection;

        public GetAllCompaniesQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetAllCompanies>> GetAllCompanies()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT 
                                        A.id,
										A.razon_social AS name,
                                        A.numero_documento AS document_number
                                    FROM   
										nsf.empresa A
                                    WHERE A.estado = true";

                    IEnumerable<ResponseGetAllCompanies> parameters = await connection.QueryAsync<ResponseGetAllCompanies>(sql);
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
