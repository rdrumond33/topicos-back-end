
using System.ComponentModel.DataAnnotations.Schema;


namespace Projeto.Model
{
    [Table("recurso")]
    public class Recurso : BaseEntity
    {
        [Column(name: "Descricao")]
        public string Descricao { set; get; }
    }
}
