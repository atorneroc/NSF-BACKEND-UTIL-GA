using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.GetBranchOfficeByCompanyId;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetBranchOfficeByCompanyId
{
    public class GetBranchOfficeByCompanyIdQuery : IGetBranchOfficeByCompanyIdQuery
    {
        private readonly IDbConnection _connection;

        public GetBranchOfficeByCompanyIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetBranchOfficeByCompanyId>> GetBranchOfficeByCompanyId(int CompanyId)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT    distinct s.id
                                            , s.descripcion AS name
                                    FROM 	  nsf.estructura_organizacional_base eob
                                    LEFT JOIN nsf.sucursal s ON eob.id_sucursal = s.id
                                    WHERE eob.id_empresa = @CompanyId
                                    AND s.estado = true";

                    var queryArgs = new { CompanyId };

                    IEnumerable<ResponseGetBranchOfficeByCompanyId> result = await connection.QueryAsync<ResponseGetBranchOfficeByCompanyId>(sql, queryArgs);
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
