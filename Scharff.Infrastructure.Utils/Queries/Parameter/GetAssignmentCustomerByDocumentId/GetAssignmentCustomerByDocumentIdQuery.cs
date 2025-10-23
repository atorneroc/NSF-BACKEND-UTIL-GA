using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.GetAssignmentCustomerByDocumentId;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.GetAssignmentCustomerByDocumentId
{
    public class GetAssignmentCustomerByDocumentIdQuery : IGetAssignmentCustomerByDocumentIdQuery
    {
        private readonly IDbConnection _connection;

        public GetAssignmentCustomerByDocumentIdQuery(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<ResponseGetAssignmentCustomerByDocumentId>> GetAssignmentCustomerByDocumentIdAsync(int documentTypeId, int receiptTypeId)
        {
            using (IDbConnection connection = new NpgsqlConnection(_connection.ConnectionString))

                try
                {
                    var query = @"
                                SELECT
                                    ADCTO.ID AS Id,
                                    ADCTO.TIPO_DOCUMENTO_ID AS DocumentTypeId,
                                    TD.DESCRIPCION AS DocumentDescription,
                                    ADCTO.TIPO_OPERACION_ID AS OperationTypeId,
                                    TOPE.DESCRIPCION AS OperationDescription,
                                    TOPE.CODIGO_DETALLE AS OperationCode,	
                                    ADCTO.TIPO_AFECTACION_ID AS AffectationTypeId,
                                    TAFEC.DESCRIPCION AS AffectationDescription,
                                    ADCTO.ES_PRINCIPAL AS IsMain,
                                    ADCTO.ESTADO AS Status
                                FROM
                                    NSF.ASIGNACION_DOCUMENTO_CLIENTE_TIPO_OPERACION ADCTO
                                    LEFT JOIN NSF.DETALLE_PARAMETRO TD ON ADCTO.TIPO_DOCUMENTO_ID = TD.ID AND TD.CODIGO_GENERAL = '10'
                                    LEFT JOIN NSF.DETALLE_PARAMETRO TOPE ON ADCTO.TIPO_OPERACION_ID = TOPE.ID AND TOPE.CODIGO_GENERAL = 'TIPOPE'
                                    LEFT JOIN NSF.DETALLE_PARAMETRO TAFEC ON ADCTO.TIPO_AFECTACION_ID = TAFEC.ID AND TAFEC.CODIGO_GENERAL = '16'
                                WHERE
                                    ADCTO.TIPO_DOCUMENTO_ID = @DocumentTypeId 
                                    AND ADCTO.ID_TIPO_COMPROBANTE = @ReceiptTypeId
                                    AND ADCTO.ESTADO = TRUE";

                    var queryArgs = new { DocumentTypeId = documentTypeId, ReceiptTypeId= receiptTypeId };

                    IEnumerable<ResponseGetAssignmentCustomerByDocumentId> result = await connection.QueryAsync<ResponseGetAssignmentCustomerByDocumentId>(query, queryArgs);
                    return result.ToList();
                }
                catch (NpgsqlException err)
                {
                    Console.WriteLine(err);
                    throw;
                }

        }
    }
}
