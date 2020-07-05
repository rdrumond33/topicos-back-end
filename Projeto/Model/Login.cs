
using System.ComponentModel.DataAnnotations.Schema;
using Projeto.DAO;

namespace Projeto.Model
{
    [Table("login")]
    public class Login : BaseEntity
    {
        [Column(name: "Id_perfil")]
        public int Id_perfil { set; get; }

        [Column(name: "Id_pessoa")]
        public int Id_pessoa { set; get; }

        [Column(name: "Password")]
        public string Password { set; get; }

        [Column(name: "User")]
        public string User { set; get; }

        [IncludeAttribute]
        [ForeignKey("Id_pessoa")]
        public virtual Pessoa Pessoa { get; set; }
    }
}
