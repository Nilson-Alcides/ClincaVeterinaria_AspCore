using ClincaVeterinariaAspCore.Models;
using ClincaVeterinariaAspCore.Repositories.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace ClincaVeterinariaAspCore.Repositories
{
    public class AtendimentoRepository : IAtendimentoRepository
    {
        private readonly string _conexaoMySQL;
        public AtendimentoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void TestarAgenda(Atendimento agenda)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from tbAtendimento where dataAtend = @data and horaAtend = @hora ", conexao);

                cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = agenda.dataAtend;
                cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = agenda.horaAtend;


                MySqlDataReader leitor;

                leitor = cmd.ExecuteReader();

                if (leitor.HasRows)
                {
                    // while (leitor.Read())
                    /// {
                    agenda.confAgendamento = "0";
                    // }
                }

                else
                {
                    agenda.confAgendamento = "1";
                }

                conexao.Close();
            }
        }
        public IEnumerable<Atendimento> ObterTodosAtendimentos()
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string StatusAg = "\'agendado\'";
                List<Atendimento> Atendlist = new List<Atendimento>();

                MySqlCommand cmd = new MySqlCommand("SELECT t1.codAtendimento, t2.NomeVeter, t3.nomeAni, t2.TelVeter, t1.dataAtend, t1.horaAtend, " +
                                                   " t1.statusAtend from tbAtendimento as t1 " +
                                                   " INNER JOIN tbVeterinario as t2 ON t1.codVeter = t2.codVeter " +
                                                   " INNER JOIN tbAnimal as t3 ON t3.codAni = t1.codAni where statusAtend = "  + StatusAg + "; ", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Atendlist.Add(
                        new Atendimento
                        {
                            Id = Convert.ToInt32(dr["codAtendimento"]),
                            RefVeterinario = new Veterinario()
                            {
                                //Id = Convert.ToInt32(dr["tipo"]),
                                NomeVeter = (String)(dr["NomeVeter"]),
                                TelVeter = (String)(dr["TelVeter"])
                            },
                            RefAnimal = new Animal()
                            {
                                //Id = Convert.ToInt32(dr["tipo"]),
                                nomeAni = (String)(dr["nomeAni"])
                            },                        
                           
                            dataAtend = Convert.ToDateTime(dr["dataAtend"]).ToString("dd'/'MM'/'yyyy"),
                            horaAtend = (String)(dr["horaAtend"]),
                            statusAtend = (String)(dr["statusAtend"]),
                        }
                        );
                }
                return Atendlist;
            }
        }

        public IEnumerable<Atendimento> ObterTodosAtendimentosDetalhes()
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string StatusAg = "\'agendado\'";
                List<Atendimento> Atendlist = new List<Atendimento>();

                MySqlCommand cmd = new MySqlCommand("SELECT t1.codAtendimento, t2.NomeVeter, t3.nomeAni, t2.TelVeter, t2.EmailVeter," +
                                               " t5.tipo ,t4.racaAni, t1.dataAtend, t1.horaAtend, " +
                                               " t1.statusAtend from tbAtendimento as t1 " +
                                               " INNER JOIN tbVeterinario as t2 ON t1.codVeter = t2.codVeter " +
                                               " INNER JOIN tbAnimal as t3 ON t3.codAni = t1.codAni " +
                                               " INNER JOIN tbraca as t4 ON t4.codraca = t3.codraca " +
                                               " INNER JOIN tbtipo as t5 ON t5.codTipo = t4.codTipo where statusAtend = " + StatusAg + "; ", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Atendlist.Add(
                        new Atendimento
                        {
                            Id = Convert.ToInt32(dr["codAtendimento"]),
                            RefVeterinario = new Veterinario()
                            {
                                //Id = Convert.ToInt32(dr["tipo"]),
                                NomeVeter = (String)(dr["NomeVeter"]),
                                TelVeter = (String)(dr["TelVeter"]),
                                EmailVeter = (String)(dr["EmailVeter"]),
                                 
                            },
                            RefAnimal = new Animal()
                            {
                                //Id = Convert.ToInt32(dr["tipo"]),
                                nomeAni = (String)(dr["nomeAni"]),
                            },
                            
                            dataAtend = Convert.ToDateTime(dr["dataAtend"]).ToString("dd'/'MM'/'yyyy"),
                            horaAtend = (String)(dr["horaAtend"]),
                            statusAtend = (String)(dr["statusAtend"]),
                        }
                        );
                }
                return Atendlist;
            }
        }
        public void Cadastrar(Atendimento atendimento)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                string statusAtend = "agendado";

                MySqlCommand cmd = new MySqlCommand("insert into tbAtendimento(dataAtend, horaAtend, codVeter, codAni, statusAtend)" +
                                                     " values (@dataAtend, @horaAtend, @codVeter, @codAni,@statusAtend)", conexao);

                cmd.Parameters.Add("@dataAtend", MySqlDbType.VarChar).Value = atendimento.dataAtend;
                cmd.Parameters.Add("@horaAtend", MySqlDbType.VarChar).Value = atendimento.horaAtend;
                cmd.Parameters.Add("@codVeter", MySqlDbType.VarChar).Value = atendimento.RefVeterinario.Id;
                cmd.Parameters.Add("@codAni", MySqlDbType.VarChar).Value = atendimento.RefAnimal.Id;
                cmd.Parameters.Add("@statusAtend", MySqlDbType.VarChar).Value = statusAtend;

                cmd.ExecuteNonQuery();
                conexao.Clone();
            }
        }
        public void CancelarAtend(Atendimento atendimento)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string StatusAtend = "Cancelado";

                MySqlCommand cmd = new MySqlCommand("update tbAtendimento set statusAtend = @statusAtend where  codAtendimento = @codAtendimento", conexao);

                cmd.Parameters.Add("@codAtendimento", MySqlDbType.VarChar).Value = atendimento.Id;
                cmd.Parameters.Add("@statusAtend", MySqlDbType.VarChar).Value = StatusAtend;


                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public DataTable selecionaAgendaData(Atendimento atendimento)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Select * from tbAtendimento where dataAtend=@dataAtend ", conexao);
                cmd.Parameters.Add("@dataAtend", MySqlDbType.VarChar).Value = atendimento.dataAtend;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable atend = new DataTable();
                da.Fill(atend);
                conexao.Close();
                return atend;
            }
        }


        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public Atendimento ObterAtendimentos(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                string StatusAg = "\'agendado\'";
                //MySqlCommand cmd = new MySqlCommand("select * from tbAnimal where codAni=@codAni", conexao);
                MySqlCommand cmd = new MySqlCommand("SELECT t1.codAtendimento, t2.NomeVeter, t3.nomeAni, t2.TelVeter, t1.dataAtend, t1.horaAtend, " +
                                                   " t1.statusAtend from tbAtendimento as t1 " +
                                                   " INNER JOIN tbVeterinario as t2 ON t1.codVeter = t2.codVeter " +
                                                   " INNER JOIN tbAnimal as t3 ON t3.codAni = t1.codAni where statusAtend = " + StatusAg + "; ", conexao);
                cmd.Parameters.AddWithValue("@codAtendimento", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Atendimento atendimento = new Atendimento();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    atendimento.Id = Convert.ToInt32(dr["codAtendimento"]);
                    atendimento.RefVeterinario = new Veterinario()
                    {
                        //Id = Convert.ToInt32(dr["tipo"]),
                        NomeVeter = (String)(dr["NomeVeter"]),
                        TelVeter = (String)(dr["TelVeter"]),                        

                    };
                    atendimento.RefAnimal = new Animal()
                    {
                        //Id = Convert.ToInt32(dr["tipo"]),
                        nomeAni = (String)(dr["nomeAni"]),
                    };
                    atendimento.dataAtend = Convert.ToDateTime(dr["dataAtend"]).ToString("dd'/'MM'/'yyyy");
                    atendimento.horaAtend = (String)(dr["horaAtend"]);
                    atendimento.statusAtend = (String)(dr["statusAtend"]);
                }
                return atendimento;
            }
        }

        
    }
}
