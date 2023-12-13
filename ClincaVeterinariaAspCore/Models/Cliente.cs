using System.ComponentModel.DataAnnotations;

namespace ClincaVeterinariaAspCore.Models
{
    public class Cliente
    {
        [Display(Name = "Codigo")]
        public int id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatorio")]
        [Display(Name = "Nome")]
        public string nomeCli { get; set; }

        [Required(ErrorMessage = "O Telefone do cliente é obrigatorio")]
        [Display(Name = "Telefone")]
        public string telCli { get; set; }

        [Required(ErrorMessage = "O E-mail do cliente é obrigatorio")]
        [Display(Name = "E-mail")]
        public string EmailCli { get; set; }

    }
}
