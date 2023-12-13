using System.ComponentModel.DataAnnotations;

namespace ClincaVeterinariaAspCore.Models
{
    public class Animal
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Animal")]
        public string nomeAni { get; set; } 
        
        public Raca RefRaca { get; set; }

        [Display(Name = "Raca")]
        public List<Raca> ListaRaca { get; set; }

        public Cliente RefCliente { get; set; }

        [Display(Name = "Cliente")]
        public List<Cliente> ListaCliente { get; set; }
    }
}
