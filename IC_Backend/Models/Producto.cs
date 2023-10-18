
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IC_Backend.Models
{
    public partial class Producto
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [ForeignKey("usuarioId")]
        public string usuarioId { get; set; }
        public Usuario? usuario { get; set; }
        public string nombre { get; set; }

        public string descripción { get; set; }

        public double precio { get; set; }

        //public byte[] imagen { get; set; }

    }
}
