using Projeto.Model;
using Projeto.VO;
using System.Linq;

namespace Projeto.DAO
{

    public class PessoaDAO : BaseDAO<Pessoa, PessoaVO, DataBaseContext, AutoMapperProfile>
    {
        public override IQueryable<Pessoa> GetCustomWhere(IQueryable<Pessoa> query, PessoaVO filter)
        {
            if (filter.nome == null) return query;

            query = query.Where(c => c.Nome.Contains(filter.nome));
            return query;
        }
    }
}
