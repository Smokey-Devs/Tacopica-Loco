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
                var cmd = new MySqlCommand("getAllSuppliers", conexao);
                cmd.CommandType = CommandType.StoredProcedure;

                using var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Fornecedor
                    {
                        IdFornecedor = dr.GetInt32("IdFornecedor"),
                        Nome = dr.GetString("Nome"),
                        Tel = dr.GetString("Tel"),
                        TipoProd = dr.GetString("TipoProd")
                    });
                }
            }

            return lista;
        }

            public void AdicionarFornecedor(Fornecedor fornecedor)
            {
                using var conexao = new MySqlConnection(_conexaoMySql);
                conexao.Open();
                using var cmd = new MySqlCommand("addSupplier", conexao);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@supplierName", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@supplierPhone", fornecedor.Tel);
                cmd.Parameters.AddWithValue("@productType", fornecedor.TipoProd);
                cmd.ExecuteNonQuery();
            }
            
            public void ApagarFornecedor(int id)
            {
                using var conexao = new MySqlConnection(_conexaoMySql);
                conexao.Open();
                using var cmd = new MySqlCommand("DELETE FROM tbFornecedor WHERE idFornecedor = @id", conexao);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            public void EditarFornecedor(Fornecedor fornecedor)
            {
                using var conexao = new MySqlConnection(_conexaoMySql);
                conexao.Open();
                var cmd = new MySqlCommand("UPDATE tbFornecedor SET Nome = @nome, Tel = @tel, TipoProd = @tipoProd WHERE IdFornecedor = @id", conexao);
    
                cmd.Parameters.AddWithValue("@id", fornecedor.IdFornecedor);
                cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@tel", fornecedor.Tel);
                cmd.Parameters.AddWithValue("@tipoProd", fornecedor.TipoProd);

                cmd.ExecuteNonQuery();
            }
            
            public Fornecedor ObterFornecedor(int id)
            {
                using (var conexao = new MySqlConnection(_conexaoMySql))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbFornecedor WHERE IdFornecedor = @id", conexao);
                    cmd.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Fornecedor fornecedor = new Fornecedor();

                    if (dr.Read())
                    {
                        fornecedor.IdFornecedor = Convert.ToInt32(dr["IdFornecedor"]);
                        fornecedor.Nome = dr.GetString("Nome");
                        fornecedor.Tel = dr.GetString("Tel");
                        fornecedor.TipoProd = dr.GetString("TipoProd");
                    }

                    return fornecedor;
                }
            }
    }
}