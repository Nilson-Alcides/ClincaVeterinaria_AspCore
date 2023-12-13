using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories.Contract;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ClincaVeterinariaAspCore.Repositories
{
    public class RacaRepository : IRacaRepository
    {
       
        private readonly string _conexaoMySQL;

        public RacaRepository(IConfiguration conf)
        {

            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public IEnumerable<Raca> ObterTodosRaca()
        {
            try
            {
                List<Raca> RacaList = new List<Raca>();
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                   //// MySqlCommand cmd = new MySqlCommand("Select * from tbRaca Inner join tbTipo on tbRaca.codTipo = tbTipo.codTipo", conexao);
                    MySqlCommand cmd = new MySqlCommand("Select tbRaca.codRaca, tbRaca.racaAni,tbTipo.tipo from tbRaca Inner join tbTipo on tbRaca.codTipo = tbTipo.codTipo", conexao);
                    
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    conexao.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        RacaList.Add(
                            new Raca
                            {
                                Id = Convert.ToInt32(dr["codRaca"]),
                                //codTipo = Convert.ToInt32(dr["codTipo"]),
                                racaAni = (String)(dr["racaAni"]),
                                RefTipoAnimal = new TipoAnimal()
                                {
                                   // Id = Convert.ToInt32(dr["tipo"]),
                                    Tipo = (String)(dr["tipo"])
                                },
                            }
                            );
                    }
                    return RacaList;
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void Cadastrar(Raca raca)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbRaca(racaAni, codRaca, codTipo) values(@racaAni, @codRaca, @codTipo)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@racaAni", MySqlDbType.VarChar).Value = raca.racaAni;
                cmd.Parameters.Add("@codRaca", MySqlDbType.VarChar).Value = raca.Id;
                cmd.Parameters.Add("@codTipo", MySqlDbType.VarChar).Value = raca.RefTipoAnimal.Id;
               

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Atualizar(Raca raca)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("update tbRaca set racaAni=@racaAni, codTipo=@codTipo where codRaca=@codRaca", conexao);

                cmd.Parameters.Add("@codRaca", MySqlDbType.VarChar).Value = raca.Id;
                cmd.Parameters.Add("@racaAni", MySqlDbType.VarChar).Value = raca.racaAni;
                cmd.Parameters.Add("@codTipo", MySqlDbType.VarChar).Value = raca.RefTipoAnimal.Id;


                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbRaca where codRaca=@codRaca", conexao);
                cmd.Parameters.AddWithValue("@codRaca", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Raca ObterRaca(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select tbRaca.codRaca, tbRaca.racaAni,tbTipo.tipo from tbRaca Inner join tbTipo on tbRaca.codTipo = tbTipo.codTipo", conexao);
                cmd.Parameters.AddWithValue("@codRaca", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Raca raca = new Raca();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    raca.Id = Convert.ToInt32(dr["codRaca"]);
                    raca.racaAni = (string)dr["racaAni"];
                    raca.RefTipoAnimal = new TipoAnimal();
                    raca.RefTipoAnimal.Tipo = (string)dr["tipo"];

                }
                return raca;
            }
        }      
       
    }
}
