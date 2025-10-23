using Dapper;
using Npgsql;
using Scharff.Domain.Entities;
using System.Data;
using System.Transactions;

namespace Scharff.Infrastructure.PostgreSQL.Commands.ExchangeRate.RegisterExchangeRate
{
    public class RegisterExchangeRateCommand : IRegisterExchangeRateCommand
    {
        private readonly IDbConnection _connection;

        public RegisterExchangeRateCommand(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<int> RegisterExchangeRate(ExchangeRateModel billableOrder)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                try
                {

                    string insert = @"  INSERT INTO nsf.tipo_cambio(
										fecha, 
										compra, 
										venta, 
										usuario_creacion, 
										fecha_creacion)

										VALUES 
										(@change_date, 
										 @bank_purchase, 
										 @bank_sale, 
										 @creation_author, 
										 (SELECT current_timestamp AT TIME ZONE 'America/Lima'))

                                    RETURNING Id;";

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
    }
}
