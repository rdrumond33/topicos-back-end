
using System.ComponentModel.DataAnnotations.Schema;


namespace Projeto.Model
{
    [Table("perfil")]
    public class Perfil : BaseEntity
    {
        [Column(name: "Descricao")]
        public string Descricao { set; get; }
    }
}
