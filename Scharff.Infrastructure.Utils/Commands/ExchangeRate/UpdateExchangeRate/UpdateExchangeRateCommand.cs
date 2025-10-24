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

                    string insert = @"  UPDATE 
		                                    nsf.tipo_cambio
	                                    SET  
		                                    compra=@bank_purchase, 
		                                    venta=@bank_sale, 
		                                    usuario_modificacion=@modification_author, 
		                                    fecha_modificacion=(SELECT current_timestamp AT TIME ZONE 'America/Lima')
	                                    WHERE id =@id;";

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
