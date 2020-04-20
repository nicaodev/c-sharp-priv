using ApiCatalogo.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Context
{
    public class AppDbContext : DbContext
    {
        //Classe de contexto que vai permitir coordernar a funcionalidade do EF para o meu modelo de entidades
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {}

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
