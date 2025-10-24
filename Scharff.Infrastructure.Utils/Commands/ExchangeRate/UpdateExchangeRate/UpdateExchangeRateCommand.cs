using Dapper;
using Npgsql;
using Scharff.Domain.Entities;
using System.Data;
using System.Transactions;

namespace Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.UpdateExchangeRate
{
    public class UpdateExchangeRateCommand : IUpdateExchangeRateCommand
    {
        private readonly IDbConnection _connection;

        public UpdateExchangeRateCommand(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<int> UpdateExchangeRate(ExchangeRateModel billableOrder)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {

                    string insert =
                        "UPDATE nsf.tipo_cambio SET " +
                        "compra=" + billableOrder.bank_purchase +
                        ", venta=" + billableOrder.bank_sale +
                        ", usuario_modificacion='" + billableOrder.modification_author + "'" +
                        ", fecha_modificacion=(SELECT current_timestamp AT TIME ZONE 'America/Lima')" +
                        " WHERE id=" + billableOrder.id + ";";

                    int idInsert = await connection.ExecuteScalarAsync<int>(insert, billableOrder);
                    trans.Complete();
                    return idInsert;
                }
                catch (NpgsqlException err)
                {
                    Console.WriteLine(err);
                    throw;
                }
            }
        }
        
        // --- METODO DE PRUEBA (temporal) PARA DISPARAR CODEQL ---
        public async Task<int> UpdateExchangeRateUnsafe(string userInput)
        {
            // Ejemplo intencionalmente inseguro: concatenación directa de entrada externa
            string unsafeSql = "UPDATE nsf.tipo_cambio SET compra = " + userInput + " WHERE id = 1;";

            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                // execute with concatenated SQL (inseguro)
                var result = await connection.ExecuteScalarAsync<int>(unsafeSql);
                return result;
            }
        }

    }
}
