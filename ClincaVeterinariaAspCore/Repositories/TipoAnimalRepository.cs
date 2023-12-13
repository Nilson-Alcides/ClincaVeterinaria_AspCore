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
    public class TipoAnimalRepository : ITipoAnimalRepository
    {
        private readonly string _conexaoMySQL;
        public TipoAnimalRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        //Listar tipos
        public IEnumerable<TipoAnimal> ObterTodosTipoAnimal()
        {
            List<TipoAnimal>TipoAnimalList = new List<TipoAnimal>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                //MySqlCommand cmd = new MySqlCommand("Select * from tbRaca Inner join tbTipo on tbRaca.codTipo = tbTipo.codTipo");
                MySqlCommand cmd = new MySqlCommand("Select * from tbTipo", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    TipoAnimalList.Add(
                        new TipoAnimal
                        {
                            Id = Convert.ToInt32(dr["codTipo"]),
                            Tipo = Convert.ToString(dr["tipo"]),
                        }
                        );
                }
                return TipoAnimalList;
            }
        }
        // Cadastrar tipo
        public void Cadastrar(TipoAnimal tipoAnimal)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbTipo(tipo) values(@tipo)", conexao); // @: PARAMETRO

                cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = tipoAnimal.Tipo;


                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
        // Atualizar tipo
        public void Atualizar(TipoAnimal tipoAnimal)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("update tbTipo set tipo=@tipo where codTipo=@codTipo", conexao);

                cmd.Parameters.Add("@codTipo", MySqlDbType.Int32).Value = tipoAnimal.Id;
                cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = tipoAnimal.Tipo;

                cmd.ExecuteNonQuery();
                
                conexao.Close();
            }

        }
        //Excluir Tipo 
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("delete from tbTipo where codTipo=@codTipo", conexao);
                cmd.Parameters.AddWithValue("@codTipo", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }
        //Obter tipo por ID
        public TipoAnimal ObterTipoAnimal(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbTipo where codTipo=@codTipo", conexao);
                cmd.Parameters.AddWithValue("@codTipo", Id);
                
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                TipoAnimal tipoAni = new TipoAnimal();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    tipoAni.Id = Convert.ToInt32(dr["codTipo"]);
                    tipoAni.Tipo = (string)dr["tipo"];                    

                }
                return tipoAni;
            }

        }
       
    }
}
