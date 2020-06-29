
using System.ComponentModel.DataAnnotations.Schema;


namespace Projeto.Model
{
    [Table("pessoa_grupo")]
    public class PessoaGrupo : BaseEntity
    {
        [Column(name: "Ano")]
        public int Ano { set; get; }

        [Column(name: "Id_grupo")]
        public int Id_grupo { set; get; }

        [Column(name: "Id_pessoa")]
        public int Id_Pessoa { set; get; }

        [Column(name: "Papel")]
        public string Papel { set; get; }

        [Column(name: "Semestre")]
        public int Semestre { set; get; }


    }
}
