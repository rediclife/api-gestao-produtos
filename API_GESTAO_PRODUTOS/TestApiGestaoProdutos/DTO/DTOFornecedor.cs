using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiGestaoProdutos.DTO
{
    public class DTOFornecedor
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Cnpj { get; set; }
        public string UserId { get; set; }
    }
}
