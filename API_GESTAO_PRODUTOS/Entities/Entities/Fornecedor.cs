using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("TB_FORNECEDOR")]
    public class Fornecedor : Notifies
    {
        [Column("FRN_ID")]
        public int Id { get; set; }

        [Column("FRN_DESCRICAO")]
        [MaxLength(150)]
        public string Descricao { get; set; }

        [Column("FRN_CNPJ")]
        [MaxLength(20)]
        public string Cnpj { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column("FRN_UserId")]
        public string UserId { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Produto> Produtos { get; set; }
    }
}
