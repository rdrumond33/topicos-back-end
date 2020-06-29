using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.VO
{
    public class PessoaGrupoVO : BaseVO
    {
        public string papel { set; get; }

        public int semestre { set; get; }

        public int ano { set; get; }

        public int id_grupo { set; get; }

        public int id_pessoa { set; get; }
    }
}
