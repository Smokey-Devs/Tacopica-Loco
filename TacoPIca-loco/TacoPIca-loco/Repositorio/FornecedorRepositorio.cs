using MySql.Data.MySqlClient;
using System.Data;
using TacoPIca_loco.Models;

namespace TacoPIca_loco.Repositorio
{
    public class FornecedorRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySql = configuration.GetConnectionString("database");

        public List<Fornecedor> TodosFornecedores()
        {
            var lista = new List<Fornecedor>();

            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                var cmd = new MySqlCommand("SELECT * FROM tbFornecedor", conexao);

                using var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Fornecedor
                    {
                        IdFornecedor = dr.GetInt32("IdFornecedor"),
                        Nome = dr.GetString("Nome"),
                        Tel = dr.GetString("Tel"),
                        TipoProd = dr.GetString("TipoProd"),
                        IdPrato = dr.GetInt32("IdPrato")
                    });
                }
            }

            return lista;
        }
    }
}