using MySql.Data.MySqlClient;
using System.Data;
using TacoPIca_loco.Models;

namespace TacoPIca_loco.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySql = configuration.GetConnectionString("database");

        public void Cadastrar(Cardapio produto)
        {
            //if (produto == null){}
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbCardapio(nome, ingredientes, preco, codigobr, descricao, categoria, coditemlote) values (@nome, @preco, @descricao)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Cardapio produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySql))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("update tbCardapio set Nome=@nome, Descricao=@descricao, Preco=@preco where IdPrato = @id", conexao);
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = produto.IdPrato;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao Atualizar o Produto: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Cardapio> TodosProdutos()
        {
            List<Cardapio> produtos = new List<Cardapio>();

            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tbCardapio", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    produtos.Add(
                            new Cardapio()
                            {
                                IdPrato = Convert.ToInt32(dr["IdPrato"]),
                                Nome = ((string)dr["Nome"]),
                                Descricao = ((string)dr["Descricao"]),
                                Preco = Convert.ToDecimal(dr["Preco"])
                            });
                }
                return produtos;
            }
        }

        public Cardapio ObterProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from tbCardapio where IdPrato = @id", conexao);
                cmd.Parameters.AddWithValue("id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Cardapio produto = new Cardapio();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.IdPrato = Convert.ToInt32(dr["IdPrato"]);
                    produto.Nome = (string)(dr["Nome"]);
                    produto.Descricao = (string)(dr["Descricao"]);
                    produto.Preco = Convert.ToDecimal(dr["Preco"]);
                }
                return produto;
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbCardapio where IdPrato = @id", conexao);
                cmd.Parameters.AddWithValue("idPrato", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}