using Dapper;
using Npgsql;
using Scharff.Domain.Response.Service.GetAllService;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Service.GetAllService
{
    public class GetAllServiceQuery : IGetAllServiceQuery
    {
        private readonly IDbConnection _connection;

        public GetAllServiceQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetAllService>> GetAllService()
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @" SELECT id, descripcion description FROM nsf.servicio where estado = true order by 2 asc";

                    IEnumerable<ResponseGetAllService> parameters = await connection.QueryAsync<ResponseGetAllService>(sql);
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
