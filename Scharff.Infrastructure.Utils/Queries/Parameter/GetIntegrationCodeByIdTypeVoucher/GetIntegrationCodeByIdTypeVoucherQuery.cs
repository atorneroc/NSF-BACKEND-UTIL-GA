using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.GetIntegrationCodeByIdTypeVoucher;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetIntegrationCodeByIdTypeVoucher
{
    public class GetIntegrationCodeByIdTypeVoucherQuery : IGetIntegrationCodeByIdTypeVoucherQuery
    {
        private readonly IDbConnection _connection;

        public GetIntegrationCodeByIdTypeVoucherQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ResponseGetIntegrationCodeByIdTypeVoucher>> GetIntegrationCodeByIdTypeVoucher(int idTypeVoucher)
        {
            try
            {
                string sql = @"
                                SELECT 
                                id_tipo_comprobante AS id_type_voucher,
                                codigo_integracion_cliente AS customer_integration_code,
                                estado AS state
                                FROM 
                                    nsf.tipo_comprobante_codigo_integracion
                                WHERE
                                    id_tipo_comprobante =  @idTypeVoucher;";

                var parameters = new { idTypeVoucher };

                var result = await _connection.QueryAsync<ResponseGetIntegrationCodeByIdTypeVoucher>(sql, parameters);
                return result.ToList();
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Error al consultar la configuración de comprobante de pago: {ex.Message}", ex);
            }
        }
    }
}