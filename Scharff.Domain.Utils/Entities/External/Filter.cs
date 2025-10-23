namespace Scharff.Domain.Entities.External
{
    public class Filter
    {   
        public Filter() { 
            Company = new Empresa();
            Product = new Producto();
            Branch = new Sucursal();
            BusinessUnit = new UnidadNegocio();
            Service = new Servicio();      
        }
        public Empresa Company { get; set; }
        public Producto Product { get; set; }
        public Sucursal Branch { get; set; }
        public UnidadNegocio BusinessUnit { get; set; }
        public Servicio Service { get; set; }
    }
}
