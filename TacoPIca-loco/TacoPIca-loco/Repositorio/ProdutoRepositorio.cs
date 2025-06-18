using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using TacoPIca_loco.Models;

namespace TacoPIca_loco.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySql = configuration.GetConnectionString("database");

        public void AdicionarProduto(Cardapio produto)
        {
            using var conexao = new MySqlConnection(_conexaoMySql);
            conexao.Open();

            using var cmd = new MySqlCommand("addDish", conexao);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", produto.Nome);
            cmd.Parameters.AddWithValue("@description", produto.Descricao);
            cmd.Parameters.AddWithValue("@price", produto.Preco);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Cardapio> TodosProdutos()
        {
            List<Cardapio> produtos = new List<Cardapio>();

            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("getAllDishes", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public void ApagarProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM tbCardapio WHERE IdPrato = @id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        
        public void EditarProduto(Cardapio produto)
        {
            using var conexao = new MySqlConnection(_conexaoMySql);
            conexao.Open();

            var cmd = new MySqlCommand("UPDATE tbCardapio SET Nome = @nome, Descricao = @descricao, Preco = @preco WHERE IdPrato = @id", conexao);
            cmd.Parameters.AddWithValue("@id", produto.IdPrato);
            cmd.Parameters.AddWithValue("@nome", produto.Nome);
            cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
            cmd.Parameters.AddWithValue("@preco", produto.Preco);

            cmd.ExecuteNonQuery();
        }

    }
}