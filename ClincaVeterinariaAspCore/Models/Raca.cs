using ClincaVeterinariaAspCore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClincaVeterinariaAspCore.Models
{
    public class Raca
    {
        [Display(Name = "Código")]
        public int Id { get; set; }        

        [Display(Name = "Raça")]
        public string racaAni { get; set; }

        [Display(Name = "Código Tipo")]
        public TipoAnimal RefTipoAnimal { get; set; }

        public List<TipoAnimal> ListaTipo { get; set; }
    }
}
