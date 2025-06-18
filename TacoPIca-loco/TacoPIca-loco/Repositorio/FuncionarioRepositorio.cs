using System.Data;
using MySql.Data.MySqlClient;
using TacoPIca_loco.Models;

namespace TacoPIca_loco.Repositorio
{
    public class FuncionarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySql = configuration.GetConnectionString("database");

        public List<Funcionario> TodosFuncionarios()
        {
            var lista = new List<Funcionario>();

            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                var cmd = new MySqlCommand("getAllEmployees", conexao);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Funcionario
                    {
                        IdFuncionario = dr.GetInt32("IdFuncionario"),
                        Nome = dr["Nome"] as string,
                        Email = dr["Email"] as string,
                        IdUsuario = dr.GetInt32("IdUsuario")
                    });
                }
            }

            return lista;
        }

        public void AdicionarFuncionario(Funcionario funcionario)
        {
            using var conexao = new MySqlConnection(_conexaoMySql);
            conexao.Open();
            using var cmd = new MySqlCommand("addFunc", conexao);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@funcNome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@funcEmail", funcionario.Email);
            cmd.Parameters.AddWithValue("@userId", 1);

            cmd.ExecuteReader();
        }

        public void ApagarFuncionario(int id)
        {
            using var conexao = new MySqlConnection(_conexaoMySql);
            conexao.Open();
            using var cmd = new MySqlCommand("DELETE FROM tbFuncionario WHERE IdFuncionario = @id", conexao);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public Funcionario ObterFuncionario(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbFuncionario WHERE idFuncionario = @id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Funcionario funcionario = new Funcionario();

                if (dr.Read())
                {
                    funcionario.IdFuncionario = Convert.ToInt32(dr["IdFuncionario"]);
                    funcionario.Nome = dr.GetString("Nome");
                    funcionario.Email = dr.GetString("Email");
                }

                return funcionario;
            }
        }
        
        public void EditarFuncionario(Funcionario funcionario)
        {
            using var conexao = new MySqlConnection(_conexaoMySql);
            conexao.Open();
            var cmd = new MySqlCommand("UPDATE tbFuncionario SET Nome = @nome, Email = @email WHERE IdFuncionario = @id", conexao);
    
            cmd.Parameters.AddWithValue("@id", funcionario.IdFuncionario);
            cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@email", funcionario.Email);

            cmd.ExecuteNonQuery();
        }
    }
}