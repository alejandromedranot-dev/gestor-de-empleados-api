namespace MiWebAPI.Models
{
    public class ProductoDetalle
    {
        public long Id { get; set; }
        public string? nombre_producto { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string? descripcion { get; set; }
    }
}
