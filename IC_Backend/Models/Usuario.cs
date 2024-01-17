using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IC_Backend.Models
{
    public partial class Usuario : IdentityUser
    {

        public string celular { get; set; }

        public ICollection<Producto>? productos { get; set; }
        public ICollection<Compra>? productosComprados { get; set; }
        public ICollection<Compra>? productosVendidos { get; set; }
    }
}

