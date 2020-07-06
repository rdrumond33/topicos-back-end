using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Model
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
        {

        }
        public virtual DbSet<Anotacao> Anotacao { get; set; }

        public virtual DbSet<Pessoa> Pessoa { get; set; }

        public virtual DbSet<Recurso> Recurso { get; set; }

        public virtual DbSet<Grupo> Grupo { get; set; }

        public virtual DbSet<Perfil> Perfil { get; set; }

        public virtual DbSet<PerfilRecurso> PerfilRecurso { get; set; }

        public virtual DbSet<PessoaGrupo> PessoaGrupo { get; set; }

        public virtual DbSet<Login> Login { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //o correto é colocar isso no arquivo de configuração
                optionsBuilder.UseMySql("server=localhost;database=rodrigo;user=root;password=root");
            }
        }

    }
}
