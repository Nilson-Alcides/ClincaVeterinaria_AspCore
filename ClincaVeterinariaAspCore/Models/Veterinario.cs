using System.ComponentModel.DataAnnotations;

namespace ClincaVeterinariaAspCore.Models
{
    public class Veterinario
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Veterinario")]
        public string NomeVeter { get; set; }

        [Display(Name = "Telefone")]
        public string TelVeter { get; set; }

        [Display(Name = "E-mail")]
        public string EmailVeter { get; set; }
    }
}
