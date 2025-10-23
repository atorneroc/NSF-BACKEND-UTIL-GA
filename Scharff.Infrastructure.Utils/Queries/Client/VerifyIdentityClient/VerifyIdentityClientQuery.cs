using Dapper;
using Npgsql;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Client.VerifyIdentityClient
{
    public class VerifyIdentityClientQuery : IVerifyIdentityClientQuery
    {
        private readonly IDbConnection _connection;

        public VerifyIdentityClientQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> VerifyIdentityClient(int documentTypeId, string? numberDocumentIdentity)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {
                    string sql = @"SELECT c.id 
                                    FROM NSF.CLIENT C
                                    WHERE C.identity_document_number = @numberDocumentIdentity AND C.document_type_id=@documentTypeId";

                    var queryArgs = new { numberDocumentIdentity, documentTypeId };

                    return await connection.QueryFirstOrDefaultAsync<bool>(sql, queryArgs);
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