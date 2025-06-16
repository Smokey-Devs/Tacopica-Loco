using System.Collections.Generic;
using TacoPIca_loco.Models;

namespace TacoPIca_loco.Models.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Cardapio> Pratos { get; set; }
        public IEnumerable<Funcionario> Funcionarios { get; set; }
        public IEnumerable<Fornecedor> Fornecedores { get; set; }
    }
}