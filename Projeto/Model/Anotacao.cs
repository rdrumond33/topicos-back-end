
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Model
{
    [Table("anotacao")]
    public class Anotacao : BaseEntity
    {
        [Column(name: "Obs")]
        public string Obs { set; get; }

        [Column(name: "Data")]
        public System.DateTime Data { set; get; }

        [Column(name: "Id_grupo")]
        public int Id_grupo { set; get; }

        [Column(name: "Id_pessoa")]
        public int Id_pessoa { set; get; }
    }
}
