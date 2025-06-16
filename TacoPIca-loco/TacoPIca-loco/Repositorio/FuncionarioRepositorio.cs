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
                var cmd = new MySqlCommand("SELECT * FROM tbFuncionario", conexao);

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
    }
}