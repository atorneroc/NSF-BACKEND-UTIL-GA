using Dapper;
using Npgsql;
using Scharff.Domain.Response.Parameter.ValidateConfiguredService;
using System.Data;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Parameter.ValidateConfiguredService
{
    public class ValidateConfiguredServiceQuery : IValidateConfiguredServiceQuery
    {
        private readonly IDbConnection _connection;

        public ValidateConfiguredServiceQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> GetOrganizationStructure(int? idCompany, int? idBranch, int? idBusinessUnit, string serviceTypeCode)
        {
            try
            {
                const string query = @"SELECT c.id
                                       FROM nsf.configuracion_operativa_estructura_organizacion c
                                       LEFT JOIN nsf.detalle_parametro DTPD ON c.id_tipo_servicio = DTPD.id AND DTPD.codigo_general = 'CODTIPOSERVICIO'
                                       WHERE c.id_empresa = @idCompany
                                       AND c.id_sucursal = @idBranch
                                       AND c.id_unidad_negocio = @idBusinessUnit
                                       AND DTPD.codigo_detalle = @serviceTypeCode";

                var configuracionId = await _connection.QueryFirstOrDefaultAsync<int>(query, new { idCompany, idBranch, idBusinessUnit, serviceTypeCode });

                return configuracionId;
            }
            catch (NpgsqlException err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public async Task<List<ConfiguredServiceResponse>> GetOrganizationStructureService(int idOrganizationStructure)
        {
            try
            {
                const string query = @"SELECT 
                                       CS.id AS Id,
                                       CS.id_configuracion_operativa_estructura_organizacion AS Id_Operational_Structure_Config,
                                       CS.id_producto AS Id_Product,
                                       PR.codigo_integracion_of AS Product_Detail,
                                       CS.id_tipo_facturar AS Id_Billing_Type,
                                       DTPDF.codigo_detalle AS Billing_Type_Detail,
                                       CS.id_tipo_gasto_ingreso AS Id_Expense_Income_Type,
                                       DTPDI.codigo_detalle AS Expense_Income_Type_Detail,
                                       CS.id_estructura_organizacional_servicio AS Id_Organizational_Service_Structure
                                   FROM nsf.configuracion_operativa_estructura_organizacion_servicio CS
                                   LEFT JOIN nsf.producto PR ON PR.id_producto = CS.id_producto
                                   LEFT JOIN nsf.detalle_parametro DTPDF ON CS.id_tipo_facturar = DTPDF.id AND DTPDF.codigo_general = 'CODTIPOFACTURAR' AND DTPDF.estado = true
                                   LEFT JOIN nsf.detalle_parametro DTPDI ON CS.id_tipo_gasto_ingreso = DTPDI.id AND DTPDI.codigo_general = 'CODTIPGASTOINGRESO' AND DTPDI.estado = true
                                   WHERE id_configuracion_operativa_estructura_organizacion = @idOrganizationStructure";

                var result = await _connection.QueryAsync<ConfiguredServiceResponse>(query, new { idOrganizationStructure });

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