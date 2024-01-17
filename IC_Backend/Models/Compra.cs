using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IC_Backend.Models
{
    public class Compra
    {
        [Key] 
        public string? Id { get; set; }

        [Required]
        [ForeignKey("usuarioCompraId")]
        public string usuarioCompraId { get; set; }

        [Required]
        [ForeignKey("usuarioVentaId")]
        public string usuarioVentaId { get; set; }

        [Required]
        [ForeignKey("productoId")]
        public string productoId { get; set; }

        public int cantidad { get; set; }
        public double total { get; set; }
        [DataType(DataType.Date)]
        public DateTime? fecha { get; set; }

        public Producto? producto { get; set; }
        public Usuario? usuarioCompra { get; set; }
        public Usuario? usuarioVenta { get; set; }
    }
}
