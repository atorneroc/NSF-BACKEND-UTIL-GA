using Scharff.Domain.Response.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;

namespace Scharff.Infrastructure.PostgreSQL.Queries.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio
{
    public interface IGetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery
    {
        Task<List<ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio>> GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio(int id_empresa, int id_sucursal, int id_unidad_negocio);
    }
}
