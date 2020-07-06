using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.VO
{
    public class AnotacaoVO : BaseVO
    {
        public string  obs { set; get; }

        public System.DateTime data { set; get; }

        public int id_grupo { set; get; }

        public int id_pessoa { set; get; }
        public PessoaVO Pessoa { set; get; }
                public GrupoVO Grupo { set; get; }

    }
}
