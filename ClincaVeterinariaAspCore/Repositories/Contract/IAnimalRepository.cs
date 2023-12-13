using ClincaVeterinariaAspCore.Models;

namespace ClincaVeterinariaAspCore.Repositories.Contract
{
    public interface IAnimalRepository
    {
        //CRUD
        IEnumerable<Animal> ObterTodosAnimais();
        void Cadastrar(Animal animal);
        void Atualizar(Animal animal);
        Animal ObterAnimais(int Id);        
        void Excluir(int Id);
    }
}
