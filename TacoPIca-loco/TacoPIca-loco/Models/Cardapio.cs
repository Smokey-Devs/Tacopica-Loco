namespace TacoPIca_loco.Models;

public class Cardapio
{
    public int IdPrato { get; set; }
    public string? Nome { get; set; }
    public decimal? Preco { get; set; }
    public string? Descricao { get; set; }
}