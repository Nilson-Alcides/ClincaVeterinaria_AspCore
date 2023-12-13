using ClincaVeterinariaAspCore.Models;
using System.Collections.Generic;

namespace ClincaVeterinariaAspCore.Repositories.Contract
{
    public interface IRacaRepository
    {
        //CRUD
        void Cadastrar(Raca raca);
        void Atualizar(Raca raca);
        void Excluir(int Id);
        Raca ObterRaca(int Id);
        IEnumerable<Raca> ObterTodosRaca();
    }
}
