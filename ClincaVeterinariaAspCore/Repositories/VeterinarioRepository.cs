using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;

namespace ClincaVeterinariaAspCore.Repositories
{
    public class VeterinarioRepository : IVeterinarioRepository
    {
        private readonly string _conexaoMySQL;
        public VeterinarioRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
                
        public IEnumerable<Veterinario> ObterTodosVeterinario()
        {
            List<Veterinario> veterList = new List<Veterinario>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tbVeterinario", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    veterList.Add(
                        new Veterinario
                        {
                            Id = Convert.ToInt32(dr["codVeter"]),
                            NomeVeter = Convert.ToString(dr["NomeVeter"]),
                            TelVeter = Convert.ToString(dr["TelVeter"]),
                            EmailVeter = Convert.ToString(dr["EmailVeter"])
                        }
                        );
                }
                return veterList;
            }
        }

        
        public void Cadastrar(Veterinario veterinario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
               
                MySqlCommand cmd = new MySqlCommand("insert into tbVeterinario(NomeVeter, TelVeter, EmailVeter) values(@NomeVeter," +
                    " @TelVeter, @EmailVeter)", conexao); // @: PARAMETRO


                cmd.Parameters.Add("@NomeVeter", MySqlDbType.VarChar).Value = veterinario.NomeVeter;
                cmd.Parameters.Add("@TelVeter", MySqlDbType.VarChar).Value = veterinario.TelVeter;
                cmd.Parameters.Add("@EmailVeter", MySqlDbType.VarChar).Value = veterinario.EmailVeter;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
       
        public void Atualizar(Veterinario veterinario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update tbVeterinario set NomeVeter=@NomeVeter, TelVeter=@TelVeter, " +
                                             "EmailVeter=@EmailVeter where codVeter=@codVeter", conexao);


                cmd.Parameters.Add("@codVeter", MySqlDbType.Int32).Value = veterinario.Id;
                cmd.Parameters.Add("@NomeVeter", MySqlDbType.VarChar).Value = veterinario.NomeVeter;
                cmd.Parameters.Add("@TelVeter", MySqlDbType.VarChar).Value = veterinario.TelVeter;
                cmd.Parameters.Add("@EmailVeter", MySqlDbType.VarChar).Value = veterinario.EmailVeter;
                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public Veterinario ObterVeterinario(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbVeterinario where codVeter=@codVeter", conexao);
                cmd.Parameters.AddWithValue("@codVeter", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Veterinario veterinario = new Veterinario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    veterinario.Id = Convert.ToInt32(dr["codVeter"]);
                    veterinario.NomeVeter = Convert.ToString(dr["NomeVeter"]);
                    veterinario.TelVeter = Convert.ToString(dr["TelVeter"]);
                    veterinario.EmailVeter = Convert.ToString(dr["EmailVeter"]);

                }
                return veterinario;
            }
        }
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbVeterinario where codVeter=@codVeter", conexao);
                cmd.Parameters.AddWithValue("@codVeter", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }        
       
    }
}
