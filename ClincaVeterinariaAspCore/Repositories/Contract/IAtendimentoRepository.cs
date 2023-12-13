using aspnetcoremvc_adonet.Models;
using System.Data;

namespace aspnetcoremvc_adonet.Repositories.Contract
{
    public interface IAtendimentoRepository
    {
        //CRUD

        IEnumerable<Atendimento> ObterTodosAtendimentos();
        IEnumerable<Atendimento> ObterTodosAtendimentosDetalhes();
        void TestarAgenda(Atendimento agenda);
        void Cadastrar(Atendimento atendimento);
        
      //  void Atualizar(Atendimento atendimento);
        Atendimento ObterAtendimentos(int Id);
        void Excluir(int Id);

        void CancelarAtend(Atendimento atendimento);
        DataTable selecionaAgendaData(Atendimento atendimento);

    }
}
