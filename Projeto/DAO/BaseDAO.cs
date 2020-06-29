using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projeto.Model;
using Projeto.VO;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DAO
{
    public abstract class BaseDAO<Entity, VO, TContext, AutoMapProfile>
        where TContext : DbContext, new()
        where AutoMapProfile : Profile, new()
        where Entity : BaseEntity, new()
        where VO : BaseVO, new()
    {

        protected IMapper _mapper;

        public BaseDAO()
        {
            // O mapper mapeia o json que ta sendo passado por parametro pra um objeto
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapProfile());
            });

            _mapper = new Mapper(config);
        }

        //Este método recebe qualquer objeto que tenha herdado de BaseVO
        //Isto está sendo sinalizado por : where VO : BaseVO, new()
        public virtual void Save(VO vo)
        {
            Entity entity = null;
            try
            {
                /*Basicamente o using serve para fazer o dispose automático de um objeto.
                 * No exemplo abaixo, fora do using não existirá o TContext.
                 * Objetos do tipo DbContext fornecem recursos para consultar e trabalhar com os dados 
                 * de entidades como objetos.
                */
                using (TContext context = new TContext())
                {
                   /*
                    * Como o mapeamento junto ao banco é feito pelo model
                      e os dados transitam no sistema por VO, é  necessário 
                      fazer a transformação. 
                      
                      AutoMapper é um recurso que permite mapear um objeto em
                      outro. Da forma que estamos utilizando ele mapeia atributos
                      de mesmo nome. 
                    */
                    entity = _mapper.Map<VO, Entity>(vo, entity);

                    /*
                     * Adicionando um objeto ao contexto atual, neste momento ainda não foi persistido
                     * no banco de dados. As linhas 60 e 61 colocam a entidade no estado adicionado, o que 
                     * significa que ela será inserida no banco de dados na próxima vez que SaveChanges 
                     * for chamado.
                     * Você pode utilizar a linha 60 e/ou 61 para fazer este tipo de sinalização.
                    */
                    context.Set<Entity>().Add(entity);  
                    context.Entry(entity).State = EntityState.Added;

                    //faz as alterações no banco de dados
                    context.SaveChanges();
                }
            }
            catch
            {   // Caso ele não consiga salvar ele lança uma exceção
                throw new System.Exception("");
            }

        }

        //Para pagar uma tupla no banco de dados é necessário passar um ID
        public virtual bool Delete(long id)
        {
            try
            {
                using (TContext context = new TContext())
                {   /*
                        context.set define um dbset especifico. Entity é uma classe genérica, ou seja, 
                        quando uma classe DAO herda o BaseDAO ele específica com qual Entity quer trabalhar. 

                        .where verifica se para o contexto em questão há alguma tupla com o id passado de 
                        parâmetro. FirstOrDefault obtém o primeiro valor caso o resultado seja composto 
                        por vários valores. Como ID é sempre a chave primária de uma Entity no banco de dados,
                        retornar tuplas com o mesmo ID não seria possível. 
                    */
                    var entity = context.Set<Entity>().Where(m => m.Id == id).FirstOrDefault();

                    //se há uma tupla no banco de dados que atenda a requisição acima, esse objeto será diferente
                    //de null.
                    if (entity != null)
                    {
                        //remove do contexto a entidade encontrada, lembre que isso ainda não apaga do banco.
                        context.Set<Entity>().Remove(entity);
                        //salva as alterações, ou seja, apaga essa tupla do banco de dados.
                        context.SaveChanges();
                        // retorna true caso a operação seja feita 
                        return true;
                    }
                }
            }
            catch
            {   //caso ele não consiga deletar lança uma exceção
                throw new System.Exception("");
            }

            return false;
        }

        /*
         * Diferente do método Select, que pode paginar o resultado e pode fazer um filtro por diferentes
         * atributos, este recebe apenas uma chave primária (ID) de parâmetro e verifica se existe para 
         * o contexto/entidade em questão. 
        */  
        public virtual VO SelectOne(long id)
        {
            Entity entity;
            using (TContext context = new TContext())
            {
                entity = context.Set<Entity>().Where(m => m.Id == id).FirstOrDefault();
            }
            return entity != null ? FromEntityToVO(entity) : null;
        }

        public virtual List<VO> Select(int pageNumber, int numberOfElements, VO filter)
        {
            var filteredItensVO = new List<VO>();

            using (TContext context = new TContext())
            {
                int currentPage = pageNumber == 0 ? pageNumber = 1 : pageNumber;

                IQueryable<Entity> dbQuery = context.Set<Entity>();
                dbQuery = GetCustomWhere(dbQuery, filter);

                int totalItems = dbQuery.Count();
                int itemPerPage = numberOfElements == 0 ? totalItems : numberOfElements;

                /*
                 * Skip: ignora os N primeiros elementos de uma consulta.
                 * Take: retorna os N primeiros elementos de uma consulta.
                 * Imagine um banco de dados com 1000 tuplas. Você poderia ignorar as 500 primeiras
                 * com Skip(500) e pegar as próximas 100 com Take(100). Isso quer dizer que você 
                 * pegaria as 100 primeiras tuplas a partir da 500.
                 */
                dbQuery = dbQuery
                    .Skip((currentPage - 1) * numberOfElements)
                    .Take(itemPerPage);

                IEnumerable<Entity> filteredItems = dbQuery;                 

                if(itemPerPage > 0)
                {
                    /*
                     * para cada elemento obtido na consulta, ele transforma o que será retornado (entity)
                     * em um VO. 
                    */
                    filteredItensVO = filteredItems
                        .Select(m => FromEntityToVO(m))
                        .ToList();
                }

            }
            return filteredItensVO;
        }

        public virtual bool Update(VO vo)
        {
            Entity entity = null;

            using (TContext context = new TContext())
            {
                entity = context.Set<Entity>().Where(m => m.Id == vo.id).FirstOrDefault();

                if (entity != null)
                {
                    context.Entry(entity).State = EntityState.Detached;
                    entity = FromVOToEntity(vo, entity);

                    //assim como é feito para o método save, você deve sinalizar que houve mudança
                    //no contexto, neste caso de atualização.
                    context.Set<Entity>().Update(entity);
                    context.Entry(entity).State = EntityState.Modified;

                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public virtual VO FromEntityToVO(Entity entity)
        {
            return _mapper.Map<VO>(entity);
        }

        public virtual Entity FromVOToEntity(VO vo, Entity entity)
        {
            return _mapper.Map<VO, Entity>(vo, entity);
        }

        /*
         * Este método deveria ser abstrato, obrigando qualquer classe que herde de BaseDAO 
         implemente um filtro específico para ser utilizando quando executado o Select. 
         Como a nossa implementação não está sinalizada como abstract, não será obrigatório a 
         implementação. Porém, o virtual sinaliza que se este método for sobrescrito, 
         deve ser utilizado aquele escrito na classe filha. O método da classe filha deve ser sinalizado
         como override. 
        */
        public virtual IQueryable<Entity> GetCustomWhere(IQueryable<Entity> query, VO filter)
        {
            return query;
        }
        
    }
}
