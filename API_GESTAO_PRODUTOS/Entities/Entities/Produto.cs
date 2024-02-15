using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("TB_PRODUTO")]
    public class Produto : Notifies
    {
        [Column("PRD_ID")]
        public int Id { get; set; }

        [Column("PRD_DESCRICAO")]
        [MaxLength(150)]
        public string Descricao { get; set; }

        [Column("PRD_ATIVO")]
        public bool Ativo {  get; set; }

        [Column("PRD_DATA_FABRICACAO")]
        public DateTime DataFabricacao { get; set; }

        [Column("PRD_DATA_VALIDADE")]
        public DateTime DataValidade { get; set; }

        [ForeignKey("Fornecedor")]
        [Column("PRD_FRN_ID")]
        public int FornecedorId { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column("PRD_UserId")]
        public string UserId { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
