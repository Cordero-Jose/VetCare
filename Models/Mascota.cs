using System.ComponentModel.DataAnnotations;

namespace VetCare.Models;

public class Mascota
{
    public int IdMascota { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La especie es obligatoria.")]
    [StringLength(50)]
    [Display(Name = "Especie")]
    public string Especie { get; set; } = null!;

    [StringLength(100)]
    [Display(Name = "Raza")]
    public string? Raza { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Nacimiento")]
    public DateOnly? FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El nombre del propietario es obligatorio.")]
    [StringLength(150)]
    [Display(Name = "Propietario")]
    public string NombrePropietario { get; set; } = null!;

    [StringLength(20)]
    [Display(Name = "Teléfono")]
    public string? TelefonoPropietario { get; set; }

    // Navigation property
    public virtual ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
}
