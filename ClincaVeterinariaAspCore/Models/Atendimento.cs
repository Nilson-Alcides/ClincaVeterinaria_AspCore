using System.ComponentModel.DataAnnotations;

namespace ClincaVeterinariaAspCore.Models
{
    public class Atendimento
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Data")]
        public string dataAtend { get; set; }

        [Display(Name = "Hora")]
        public string horaAtend { get; set; }
        
        [Display(Name = "Confirmação")]
        public string confAgendamento { get; set; }
       
        [Display(Name = "Status")]
        public string statusAtend { get; set; }


        [Display(Name = "Código Veterinario")]
        public Veterinario RefVeterinario { get; set; }

        public List<Veterinario> ListaVeterinario { get; set; }

        [Display(Name = "Código Animal")]
        public Animal RefAnimal { get; set; }

        public List<Animal> ListaAnimal { get; set; }



    }
}
