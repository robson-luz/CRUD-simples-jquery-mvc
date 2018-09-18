using Comercio.Models.Categorias;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Comercio.Database
{
    public class DbComercio : DbContext
    {
        public DbComercio() 
            : base("name=Comercio")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoriaMap());
        }

        public void RegistrarNovo(object entidade)
        {
            Set(entidade.GetType()).Add(entidade);
        }

        public void RegistrarAlterado(object entidade)
        {
            Entry(entidade).State = EntityState.Modified;
        }

        public void RegistrarRemovido(object entidade)
        {
            Entry(entidade).State = EntityState.Deleted;
        }

        public void Salvar()
        {
            SaveChanges();
        }
    }
}