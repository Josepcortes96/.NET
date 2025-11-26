using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FitData.Entidades
{
    public class MonitorUsuario
{
    [Key]
    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }

    public Usuario Usuario { get; set; }
}

}
