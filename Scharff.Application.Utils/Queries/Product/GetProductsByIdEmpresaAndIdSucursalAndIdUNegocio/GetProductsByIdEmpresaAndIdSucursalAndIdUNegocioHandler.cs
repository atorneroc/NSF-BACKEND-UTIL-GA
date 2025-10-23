using MediatR;
using Scharff.Domain.Response.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using Scharff.Infrastructure.PostgreSQL.Queries.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;

namespace Scharff.Application.Queries.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio
{
    public class GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioHandler : IRequestHandler<GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery, List<ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio>>
    {
        private readonly IGetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery  _Repository;

        public GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioHandler(IGetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery GetAllCountryRegionQuery)
        {
            _Repository = GetAllCountryRegionQuery;
        }

        public async Task<List<ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio>> Handle(GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery request, CancellationToken cancellationToken)
        {
            var result = await _Repository.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio(request.id_empresa, request.id_sucursal, request.id_unidad_negocio);
            return result;
        }
    }
}
