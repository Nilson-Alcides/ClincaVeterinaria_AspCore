using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;

namespace ClincaVeterinariaAspCore.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _conexaoMySQL;
        public ClienteRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> cliList = new List<Cliente>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {   
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("CALL pcd_Select_Cliente()", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    cliList.Add(
                        new Cliente
                        {
                            id = Convert.ToInt32(dr["codCli"]),
                            nomeCli = Convert.ToString(dr["nomeCli"]),
                            telCli = Convert.ToString(dr["telCli"]),
                            EmailCli = Convert.ToString(dr["EmailCli"])
                        }
                        );
                }
                return cliList;
            }
        }
        public void Cadastrar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                //MySqlCommand cmd = new MySqlCommand("CALL proc_CadCliente(@nomeCli, @telCli, @EmailCli)", conexao); // @: PARAMETRO

                MySqlCommand cmd = new MySqlCommand("insert into tbCliente(nomeCli, telCli, EmailCli) " +
                "values(@nomeCli, @telCli, @EmailCli)", conexao); // @: PARAMETRO


                cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = cliente.nomeCli;
                cmd.Parameters.Add("@telCli", MySqlDbType.VarChar).Value = cliente.telCli;
                cmd.Parameters.Add("@EmailCli", MySqlDbType.VarChar).Value = cliente.EmailCli;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Atualizar(Cliente cliente)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update tbCliente set nomeCli=@nomeCli, telCli=@telCli," +
                " EmailCli=@EmailCli where codCli=@codCli", conexao);

                cmd.Parameters.Add("@codCli", MySqlDbType.VarChar).Value = cliente.id;
                cmd.Parameters.Add("@nomeCli", MySqlDbType.VarChar).Value = cliente.nomeCli;
                cmd.Parameters.Add("@telCli", MySqlDbType.VarChar).Value = cliente.telCli;
                cmd.Parameters.Add("@EmailCli", MySqlDbType.VarChar).Value = cliente.EmailCli;

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbCliente where codCli=@codCli", conexao);
                cmd.Parameters.AddWithValue("@codCli", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Cliente ObterCliente(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCliente where codCli=@codCli", conexao);
                cmd.Parameters.AddWithValue("@codCli", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Cliente cliente = new Cliente();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    cliente.id = Convert.ToInt32(dr["codCli"]);
                    cliente.nomeCli = Convert.ToString(dr["nomeCli"]);
                    cliente.telCli = Convert.ToString(dr["telCli"]);
                    cliente.EmailCli = Convert.ToString(dr["EmailCli"]);

                }
                return cliente;
            }
        }

        
    }
}
