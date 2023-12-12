using System.ComponentModel.DataAnnotations;

namespace aspnetcoremvc_adonet.Models
{
    public class TipoAnimal
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
    }
}
