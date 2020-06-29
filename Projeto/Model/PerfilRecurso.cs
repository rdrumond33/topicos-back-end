
using System.ComponentModel.DataAnnotations.Schema;


namespace Projeto.Model
{
    [Table("perfil_recurso")]
    public class PerfilRecurso : BaseEntity
    {
        [Column(name: "Id_perfil")]
        public int Id_perfil { set; get; }

        [Column(name: "Id_recurso")]
        public int Id_recurso { set; get; }
    }
}
