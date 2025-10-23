namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.Generic
{
    public interface IGenericQuery
    {
        Task<T> GetColumnAsync<T>(string schema, string tableName, List<string> columnsToSelect, object conditions = null);
    }
}
