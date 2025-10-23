using Dapper;
using Moq;
using System.Data;
using System.Linq;
using System.Text;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.Generic
{
    public class GenericQuery : IGenericQuery
    {
        private readonly IDbConnection _connection;

        public GenericQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<T> GetColumnAsync<T>(string schema, string tableName, List<string> columnsToSelect, object conditions = null)
        {
            ValidateColumnsToSelect(columnsToSelect);

            List<string> columns = await GetColumnsNameAsync(schema, tableName);
            var validColumns = GetValidColumns(columnsToSelect, columns);
            var processedConditions = ProcessConditions(conditions);
            string sqlQuery = BuildSqlQuery(schema, tableName, string.Join(",", validColumns), processedConditions);

            return await _connection.QueryFirstOrDefaultAsync<T>(sqlQuery, processedConditions);

        }
        private void ValidateColumnsToSelect(List<string> columnsToSelect)
        {
            if (columnsToSelect == null || !columnsToSelect.Any())
            {
                throw new ArgumentException("Debe proporcionar al menos un nombre de columna para seleccionar.", nameof(columnsToSelect));
            }
        }
        private List<string> GetValidColumns(List<string> columnsToSelect, List<string> availableColumns)
        {
            var validColumns = columnsToSelect.Intersect(availableColumns).ToList();

            if (!validColumns.Any())
            {
                throw new ArgumentException("No se encontraron columnas válidas para seleccionar.", nameof(columnsToSelect));
            }

            return validColumns;
        }

        private Dictionary<string, object> ProcessConditions(object conditions)
        {
            if (conditions == null) return new Dictionary<string, object>();

            var processedConditions = new Dictionary<string, object>();
            var conditionProperties = conditions.GetType().GetProperties();

            foreach (var prop in conditionProperties)
            {
                string propertyName = prop.Name;
                object value = prop.GetValue(conditions);

                processedConditions[propertyName] = value;
            }

            return processedConditions;
        }

        private string BuildSqlQuery(string schema, string tableName, string idColumn, Dictionary<string, object> conditions)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT ")
                      .Append(idColumn)
                      .Append(" FROM ")
                      .Append(schema)
                      .Append('.')
                      .Append(tableName);

            if (conditions != null && conditions.Count > 0)
            {
                sqlBuilder.Append(" WHERE ");
                AppendConditions(sqlBuilder, conditions);
            }

            return sqlBuilder.ToString();
        }

        private void AppendConditions(StringBuilder sqlBuilder, Dictionary<string, object> conditions)
        {
            if (conditions == null || conditions.Count == 0)
            {
                return;
            }

            int count = 0;
            foreach (var kvp in conditions)
            {
                var key = kvp.Key.EndsWith("_upper") ? $"UPPER({kvp.Key.Replace("_upper", "")})"  : kvp.Key;
                sqlBuilder.Append(key)
                          .Append(" = @")
                          .Append(kvp.Key);

                if (count < conditions.Count - 1)
                {
                    sqlBuilder.Append(" AND ");
                }
                count++;
            }
        }
        private async Task<List<string>> GetColumnsNameAsync(string schema, string tableName)
        {
            string columnsQuery = @"
            SELECT column_name
            FROM information_schema.columns
            WHERE table_schema = @schema AND table_name = @tableName";

            var columns = await _connection.QueryAsync<string>(columnsQuery, new { schema, tableName });

            return columns.ToList();
        }
    }
}
