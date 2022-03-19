using System.ComponentModel.DataAnnotations;

namespace ControlIngresoGasto.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display (Name="Nombre de Categoria")]
        public string NombreCateroria { get; set; }

        [Required]
        [MaxLength(2)]
        [Display (Name = "Tipo")]
        public string Tipo { get; set; } // IN ingreso gastos

        [Required]
        public bool Estado { get; set; } // true o false



    }
}
