using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.ValidateConfiguredServiceFree;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateConfiguredServiceFree
{
    public class ValidateConfiguredServiceFreeQuery : IValidateConfiguredServiceFreeQuery
    {
        private readonly IDbConnection _connection;

        public ValidateConfiguredServiceFreeQuery(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<ConfiguredServiceFreeResponse> GetFreeConfigurationByServiceIdAsync(int organizationalServiceStructureId)
        {
            try
            {
                const string query = @"SELECT 
                                        EOS.id AS Id_Organizational_Service_Structure,
                                        SERV.id AS Id_Service,
                                        SERV.descripcion AS Description,
                                        SERV.codigo_integracion_of AS Code_Integration,
                                        SERV.id_tipo_afectacion AS Id_Type_Affectation,
                                        DTP.descripcion AS Type_Affectation_Description,
                                        SERV.id_tipo_afectacion_gratuito AS Id_Free_Type_Affectation,
                                        DTPA.descripcion AS Free_Type_Affectation_Description
                                        FROM nsf.estructura_organizacional_servicio EOS 
                                        INNER JOIN nsf.estructura_organizacional_base EO  ON EO.id = EOS.id_estructura_organizacional_base
                                        LEFT JOIN  nsf.servicio SERV  ON SERV.id = EOS.id_servicio
                                        LEFT JOIN nsf.detalle_parametro DTP ON SERV.id_tipo_afectacion = DTP.id AND DTP.codigo_general = '16' AND DTP.estado = true
                                        LEFT JOIN nsf.detalle_parametro DTPA ON SERV.id_tipo_afectacion_gratuito = DTPA.id AND DTPA.codigo_general = '16' AND DTPA.estado = true
                                        WHERE EOS.id = @organizationalServiceStructureId
                                        ORDER BY 1 DESC ";

                return await _connection.QueryFirstOrDefaultAsync<ConfiguredServiceFreeResponse>(query, new { organizationalServiceStructureId });
            }
            catch (NpgsqlException err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    }
}