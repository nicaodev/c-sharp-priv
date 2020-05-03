using ApiCatalogo.Context;
using ApiCatalogo.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCatalogoxUnitTests
{
    public class DBUnitTestMockInitializer
    {
        public DBUnitTestMockInitializer()
        {}

        public void Seed(AppDbContext context)
        {
            context.Categorias.Add(new Categoria { CategoriaId = 999, Nome = "Bebidas999", ImagemUrl = "bebidas99.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 4, Nome = "Sucos", ImagemUrl = "Sucos.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 4, Nome = "Breja", ImagemUrl = "Breja.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 5, Nome = "Dogao-xtreme", ImagemUrl = "Dogao-extreme.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 6, Nome = "Sucodoce", ImagemUrl = "Sucodoce.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 4, Nome = "Uisque", ImagemUrl = "Uisque.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 4, Nome = "Vodka", ImagemUrl = "Vodka.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 5, Nome = "PizzaTest", ImagemUrl = "PizzaTest.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 6, Nome = "BoloChocolate", ImagemUrl = "BoloChocolate.jgp" });
            context.Categorias.Add(new Categoria { CategoriaId = 6, Nome = "BoloCenoura", ImagemUrl = "BoloCenoura.jgp" });

            context.SaveChanges();
        }
    }
}
