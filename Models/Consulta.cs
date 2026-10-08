using System.ComponentModel.DataAnnotations;

namespace VetCare.Models;

public class Consulta
{
    public int IdConsulta { get; set; }

    [Required]
    public int IdMascota { get; set; }

    [Required(ErrorMessage = "La fecha de consulta es obligatoria.")]
    [Display(Name = "Fecha de Consulta")]
    [DataType(DataType.DateTime)]
    public DateTime FechaConsulta { get; set; }

    [Required(ErrorMessage = "El motivo es obligatorio.")]
    [StringLength(250)]
    [Display(Name = "Motivo")]
    public string Motivo { get; set; } = null!;

    [StringLength(500)]
    [Display(Name = "Diagnóstico")]
    public string? Diagnostico { get; set; }

    [StringLength(500)]
    [Display(Name = "Tratamiento")]
    public string? Tratamiento { get; set; }

    // Navigation property
    public virtual Mascota? Mascota { get; set; }
}
