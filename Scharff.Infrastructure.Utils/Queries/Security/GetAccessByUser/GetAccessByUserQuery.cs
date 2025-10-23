using Dapper;
using Npgsql;
using Scharff.Domain.Response.Security.GetAccessByUser;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Security.GetAccessByUser
{
    public class GetAccessByUserQuery : IGetAccessByUserQuery
    {
        private readonly IDbConnection _connection;

        public GetAccessByUserQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<GetAccessByUserResponse>> GetAccessByUser(string user_email)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);

            try
            {                
                string sql = $@"
                                 select   usr.id user_code, 
                                          r.id id_role, r.descripcion role_description,
	                                      r.privilegio privilege, 
                                          CASE r.privilegio 
	   							              WHEN true THEN 'Acceso Total' 
							              ELSE 'Acceso Restringido' 
							              END AS privilege_description, 
	                                      p.id_menu, 
                                          m.descripcion menu_description, 
                                          m.icono icon, 
                                          m.ruta route,
                                          m.id_sistema id_system,
                                          s.descripcion system_description
                                    from  nsf.usuario usr                                 INNER JOIN
	                                      nsf.rol r 		 ON usr.id_rol = r.id         INNER JOIN
	                                      nsf.permiso p	     ON r.id       = p.id_rol     INNER JOIN
	                                      nsf.menu m		 ON m.id       = p.id_menu    INNER JOIN
                                          nsf.sistema s      ON s.id       = m.id_sistema
                                    where 
                                          UPPER(usr.correo_electronico) = UPPER(@user_email)
                                    order by 6";

                var queryArgs = new { user_email };

                IEnumerable<GetAccessByUserResponse> result = await connection.QueryAsync<GetAccessByUserResponse>(sql, queryArgs);

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
