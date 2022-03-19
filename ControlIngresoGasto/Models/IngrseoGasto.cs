
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlIngresoGasto.Models
{
    public class IngrseoGasto
    {
       [Key]
        public int Id { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        
        [ForeignKey("CategoriaId")]  //para hacer una relacion 
        public Categoria Categoria { get; set; }

        [Required]
        [Display(Name ="Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(1,10000)]
       // [Display(Da ="{0:C}")]
        [Display(Name ="Valor")]
        public double Valor { get; set; }
    }
}
