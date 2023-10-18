using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IC_Backend.Models
{
    public partial class Usuario : IdentityUser
    {

        public string nombre { get; set; }

        public string celular { get; set; }


        public ICollection<Producto>? Productos { get; set; }
    }
}

