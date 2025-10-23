using MediatR;
using Scharff.Domain.Response.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio
{
    public class GetProductsByIdEmpresaAndIdSucursalAndIdUNegocioQuery : IRequest<List<ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio>>{

        public int id_empresa { get; set; }
        public int id_sucursal { get; set; } 
        public int id_unidad_negocio { get; set; }
    }
}
