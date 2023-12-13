using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories.Contract;
using MySql.Data.MySqlClient;

namespace ClincaVeterinariaAspCore.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _conexaoMySQL;
        public LoginRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void TestarUsuario(Login user)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbLogin where usuario = @usuario and senha = @Senha", conexao);

                cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.usuario;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = user.senha;

                MySqlDataReader leitor;

                leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        user.usuario = Convert.ToString(leitor["usuario"]);
                        user.senha = Convert.ToString(leitor["senha"]);
                        user.tipo = Convert.ToString(leitor["tipo"]);
                    }
                }

                else
                {
                    user.usuario = null;
                    user.senha = null;
                    user.tipo = null;
                }
                conexao.Clone();
            }

        }

        
    }
}
