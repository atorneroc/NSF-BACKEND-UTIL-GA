using Dapper;
using Npgsql;
using Scharff.Domain.Response.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.User.GetUserByProfileId
{
    public class GetUserByProfileIdQuery : IGetUserByProfileIdQuery
    {
        private readonly IDbConnection _connection;

        public GetUserByProfileIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<ResponseAllUserByProfileId>> GetUserByProfile(string profile_Id)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @"select 
		                                u.id,
                                        concat(u.id, ' ' ,u.nombre_completo) AS full_name,
		                                u.nombre_completo as name,
		                                u.correo_electronico as email
                                from nsf.usuario u 
                                inner join nsf.usuario_perfil up on (u.id = up.id_usuario )
								inner join nsf.perfil p on (up.id_perfil = p.id )
                                where p.codigo_oficial = @profile_Id;
                    "; 
                    var queryArgs = new { profile_Id };

                    IEnumerable<ResponseAllUserByProfileId> CollectionManager = await connection.QueryAsync<ResponseAllUserByProfileId>(sql, queryArgs);
                    return CollectionManager.ToList();
                }
                catch (NpgsqlException err)
                {
                    throw new Exception("-", err); ;
                }
            }
        }
    }
}
