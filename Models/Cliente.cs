using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programaloginc.Models;

public class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "Escribe el nombre del cliente.")]
    [StringLength(120)]
    [Display(Name = "Nombre del cliente")]
    public string NombreCliente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Escribe la cédula del cliente.")]
    [StringLength(20)]
    [Display(Name = "Cédula")]
    public string CedulaCliente { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Today;

    [Range(0, 120, ErrorMessage = "La edad debe estar entre 0 y 120.")]
    public int Edad { get; set; }

    [StringLength(1000)]
    [Display(Name = "Observaciones")]
    public string? Observaciones { get; set; }
}