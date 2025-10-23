using Scharff.Infrastructure.PostgreSQL.Constants;
using Scharff.Infrastructure.PostgreSQL.Queries.Parameter.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.CheckExternalExistenceUser
{
    public class CheckExternalExistenceUserQuery: ICheckExternalExistenceUserQuery
    {
        private readonly IDbConnection _connection;
        private readonly IGenericQuery _genericQuery;

        public CheckExternalExistenceUserQuery(IDbConnection connection, IGenericQuery genericQuery)
        {
            _connection = connection;
            _genericQuery = genericQuery;
        }

        public  async Task<string> CheckExternalExistenceUser(string userEmail)
        {
            var idActive = await _genericQuery.GetColumnAsync<int?>(DatabaseConstants.SCHEMA_NSF, DatabaseConstants.USER_TABLE, new List<string> { "id" }, new { correo_electronico_upper = userEmail,estado= true });

            var idInactive = await _genericQuery.GetColumnAsync<int?>(DatabaseConstants.SCHEMA_NSF, DatabaseConstants.USER_TABLE, new List<string> { "id" }, new { correo_electronico_upper = userEmail, estado = false });

            if (!idActive.HasValue && !idInactive.HasValue) throw new ValidationException("No se encontro el usuario.");

            if (idInactive.HasValue) throw new ValidationException("El usuario se encuentra inactivo.");

            return idActive.Value.ToString();
        }
    }
}
