using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitData.Entidades
{
    public class Cliente
    {
        [Key]
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        // navegación hacia Usuario
        public Usuario Usuario { get; set; }
    }
}
