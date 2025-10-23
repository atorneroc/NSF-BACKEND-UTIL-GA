using Dapper;
using Npgsql;
using Scharff.Domain.Response.PaymentVoucherConfiguration;
using System.ComponentModel.Design;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.PaymentVoucherConfiguration.GetPaymentVoucherConfiguration
{
    public class GetPaymentVoucherConfigurationQuery : IGetPaymentVoucherConfigurationQuery
    {
        private readonly IDbConnection _connection;

        public GetPaymentVoucherConfigurationQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ResponsePaymentVoucherConfiguration> GetPaymentVoucherConfiguration(int Id_Payment_Voucher_Type)
        {
            using IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString);

            try
            {

                string sql = $@"
                                SELECT 
                                    id AS Id, 
                                    id_tipo_documento AS Id_Payment_Voucher_Type, 
                                    numero_dias_creacion AS Number_Days_Creation, 
                                    numero_dias_anulacion AS Number_Days_Cancellation
	                            FROM 
                                    nsf.configuracion_comprobante_pago
                                WHERE
                                    id_tipo_documento = @Id_Payment_Voucher_Type
                                ;";

                var queryArgs = new { Id_Payment_Voucher_Type };

                var result = await connection.QueryFirstOrDefaultAsync<ResponsePaymentVoucherConfiguration>(sql, queryArgs);

                return result;
            }
            catch (NpgsqlException err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    }
}
