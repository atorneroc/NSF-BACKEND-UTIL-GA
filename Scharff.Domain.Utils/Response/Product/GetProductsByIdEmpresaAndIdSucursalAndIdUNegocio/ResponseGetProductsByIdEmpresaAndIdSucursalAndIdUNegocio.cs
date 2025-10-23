namespace Scharff.Domain.Response.Product.GetProductsByIdEmpresaAndIdSucursalAndIdUNegocio
{
    public class ResponseGetProductsByIdEmpresaAndIdSucursalAndIdUNegocio
    {
        public int id { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public int id_estructura_organizacional_base { get; set; }
    }
}
