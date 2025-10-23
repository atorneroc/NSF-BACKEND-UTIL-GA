namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.CheckExternalExistenceUser
{
    public interface ICheckExternalExistenceUserQuery
    {
      Task<string> CheckExternalExistenceUser(string userEmail);
    }
}
