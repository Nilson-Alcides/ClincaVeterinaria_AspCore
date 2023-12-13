using System.ComponentModel.DataAnnotations;

namespace ClincaVeterinariaAspCore.Models
{
    public class TipoAnimal
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
    }
}
