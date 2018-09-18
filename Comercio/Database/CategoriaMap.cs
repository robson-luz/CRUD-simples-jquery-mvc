using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Comercio.Models.Categorias;
using System.Data.Entity.ModelConfiguration;

namespace Comercio.Database
{
    public class CategoriaMap : EntityTypeConfiguration<Categoria>
    {
        public CategoriaMap()
        {
            ToTable("Categoria", "dbo");

            HasKey(c => c.IdCategoria);

            Property(c => c.IdCategoria).IsRequired();
            Property(c => c.Descricao).IsRequired();

            //HasMany(c => c.Produtos).WithRequired(p => p.Categoria).HasForeignKey(p.IdCategoria);
        }
    }
}