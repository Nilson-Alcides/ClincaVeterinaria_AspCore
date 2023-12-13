using ClincaVeterinariaAspCore.Models;

namespace ClincaVeterinariaAspCore.Repositories.Contract
{
    public interface IClienteRepository
    {
        //CRUD
        void Cadastrar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(int Id);
        Cliente ObterCliente(int Id);
        IEnumerable<Cliente> ObterTodosClientes();
    }
}
