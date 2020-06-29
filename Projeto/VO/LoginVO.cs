using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.VO
{
    public class LoginVO : BaseVO
    {
        public string password { set; get; }

        public string user { set; get; }

        public int id_perfil { set; get; }

        public int id_pessoa { set; get; }
    }
}
