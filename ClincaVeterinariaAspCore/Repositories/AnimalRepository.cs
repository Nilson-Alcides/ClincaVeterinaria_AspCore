using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;

namespace ClincaVeterinariaAspCore.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string _conexaoMySQL;
        public AnimalRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }


        public IEnumerable<Animal> ObterTodosAnimais()
        {
            List<Animal> Paclist = new List<Animal>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbanimal as t1 " +
                " INNER JOIN tbcliente as t2 ON t1.codCli = t2.codCli " +
                " INNER JOIN tbraca as t3 ON t3.codRaca = t1.codRaca", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Paclist.Add(
                        new Animal
                        {
                            Id = Convert.ToInt32(dr["codAni"]),
                            nomeAni = (string)(dr["nomeAni"]),
                            RefCliente = new Cliente()
                            {
                                id = Convert.ToInt32(dr["codCli"]),
                                nomeCli = (string)(dr["nomeCli"])
                            },
                            RefRaca = new Raca()
                            {
                                Id = Convert.ToInt32(dr["codRaca"]),
                                racaAni = (string)(dr["racaAni"])
                            }
                        }
                        );
                }
                return Paclist;
            }

        }
      
        public void Cadastrar(Animal animal)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbAnimal(nomeAni, codCli, codRaca) values(@nomeAni, @codCli, @codRaca)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@nomeAni", MySqlDbType.VarChar).Value = animal.nomeAni;
                cmd.Parameters.Add("@codCli", MySqlDbType.VarChar).Value = animal.RefCliente.id;
                cmd.Parameters.Add("@codRaca", MySqlDbType.VarChar).Value = animal.RefRaca.Id;
                
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Atualizar(Animal animal)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update tbAnimal set nomeAni=@nomeAni, codCli=@codCli, codRaca=@codRaca where codAni=@codAni", conexao);

                cmd.Parameters.Add("@codAni", MySqlDbType.VarChar).Value = animal.Id;
                cmd.Parameters.Add("@nomeAni", MySqlDbType.VarChar).Value = animal.nomeAni;
                cmd.Parameters.Add("@codCli", MySqlDbType.VarChar).Value = animal.RefCliente.id;
                cmd.Parameters.Add("@codRaca", MySqlDbType.VarChar).Value = animal.RefRaca.Id;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbAnimal where codAni=@codAni", conexao);
                cmd.Parameters.AddWithValue("@codAni", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public Animal ObterAnimais(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();               
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbanimal as t1 " +
                " INNER JOIN tbcliente as t2 ON t1.codCli = t2.codCli " +
                " INNER JOIN tbraca as t3 ON t3.codRaca = t1.codRaca where codAni=@codAni", conexao);
                cmd.Parameters.AddWithValue("@codAni", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Animal animal = new Animal();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    animal.Id = Convert.ToInt32(dr["codAni"]);
                    animal.nomeAni = (string)(dr["nomeAni"]);
                            animal.RefCliente = new Cliente()
                            {
                                id = Convert.ToInt32(dr["codCli"]),
                                nomeCli = (string)(dr["nomeCli"])
                            };
                    animal.RefRaca = new Raca()
                    {
                        Id = Convert.ToInt32(dr["codRaca"]),
                        racaAni = (string)(dr["racaAni"])
                    };
                }
                return animal;
            }
        }
        
    }
}
