
using System.ComponentModel.DataAnnotations.Schema;


namespace Projeto.Model
{
    [Table("grupo")]
    public class Grupo : BaseEntity
    {
        [Column(name: "Nome")]
        public string nome { set; get; }
    }
}
