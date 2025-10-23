using Dapper;
using Npgsql;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Security.GetVerifyMenuRoutByUserRout
{
    public class GetVerifyMenuRoutByUserRoutQuery : IGetVerifyMenuRoutByUserRoutQuery
    {
        private readonly IDbConnection _connection;

        public GetVerifyMenuRoutByUserRoutQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> GetVerifyMenuRoutByUserRout(string user_email, string rout)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);
            try
            {                
                string sql = $@"
                                 select count(1)cant
                                 from   nsf.usuario usr                               INNER JOIN
	                                    nsf.rol r 		 ON usr.id_rol = r.id         INNER JOIN
	                                    nsf.permiso p	 ON r.id       = p.id_rol	  INNER JOIN
	                                    nsf.menu m		 ON m.id       = p.id_menu    INNER JOIN
                                        nsf.sistema s ON s.id          = m.id_sistema
                                 where  usr.correo_electronico         = @user_email
                                   and  m.ruta = @rout";

                var queryArgs = new { user_email, rout };

                var cant = await connection.QuerySingleAsync<int>(sql, queryArgs);
                return cant;
            }
            catch (NpgsqlException err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    }
}
