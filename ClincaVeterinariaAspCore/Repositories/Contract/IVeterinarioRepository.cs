using ClincaVeterinariaAspCore.Models;

namespace ClincaVeterinariaAspCore.Repositories.Contract
{
    public interface IVeterinarioRepository
    {
        //CRUD
        void Cadastrar(Veterinario veterinario);
        void Atualizar(Veterinario veterinario);
        void Excluir(int Id);
        Veterinario ObterVeterinario(int Id);
        IEnumerable<Veterinario> ObterTodosVeterinario();
    }
}
