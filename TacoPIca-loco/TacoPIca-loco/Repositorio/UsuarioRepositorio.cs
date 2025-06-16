using MySql.Data.MySqlClient;
using System.Data;
using TacoPIca_loco.Models;

namespace TacoPIca_loco.Repositorio;

public class UsuarioRepositorio(IConfiguration configuration)
{   
    private readonly string _conexaoMySql = configuration.GetConnectionString("database");

    public Usuario ObterUsuario(string email)
    {
        using (var conexao = new MySqlConnection(_conexaoMySql))
        {
            conexao.Open();
            MySqlCommand cmd = new("SELECT * FROM tbUsuario WHERE Email = @email", conexao);
            cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = email;

            using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                Usuario usuario = null;
                if (dr.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = dr.GetInt32("IdUsuario"),
                        Nome = dr.GetString("Nome"),
                        Email = dr.GetString("Email"),
                        Senha = dr.GetString("Senha")
                    };
                }

                return usuario;
            }
        }
    }
}