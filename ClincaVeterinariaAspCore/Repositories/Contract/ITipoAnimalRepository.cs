using ClincaVeterinariaAspCore.Models;
using System.Collections.Generic;

namespace ClincaVeterinariaAspCore.Repositories.Contract
{
    public interface ITipoAnimalRepository
    {
        //CRUD
        void Cadastrar(TipoAnimal   tipoAnimal);
        void Atualizar(TipoAnimal tipoAnimal);
        void Excluir(int Id);
        TipoAnimal ObterTipoAnimal(int Id);
        IEnumerable<TipoAnimal> ObterTodosTipoAnimal();
    }
}
