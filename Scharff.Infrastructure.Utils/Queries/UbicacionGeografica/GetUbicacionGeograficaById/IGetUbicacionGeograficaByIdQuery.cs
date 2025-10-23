using Scharff.Domain.Response.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio;
using Scharff.Domain.Response.UbicacionGeografica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.PostgreSQL.Queries.UbicacionGeografica.GetUbicacionGeograficaById
{
    public interface IGetUbicacionGeograficaByIdQuery
    {
        Task<List<ResponseGetUbicacionGeograficaById>> GetUbicacionGeograficaById(int id);
    }
}
