
using System.ComponentModel.DataAnnotations.Schema;


namespace Projeto.Model
{
    [Table("pessoa")]
    public class Pessoa : BaseEntity
    {
        [Column(name: "Nome")]
        public string Nome { set; get; }
    }
}
