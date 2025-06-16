namespace TacoPIca_loco.Models;

public class Pedido
{   
    public int IdPedido { get; set; }
    public decimal? ValorTotal { get; set; }
    public int? idUsuario { get; set; }
    public int? idPrato { get; set; }
}