using ApiCatalogo.Context;
using ApiCatalogo.Controllers;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Repository.UnitOfWork;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ApiCatalogoxUnitTests
{
    public class CategoriasUnitTestController
    {
        private IMapper mapper;
        private IUnitOfWork repository;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionString = "Server=localhost;Database=CatalogDB;User Id=root;Password=159753";

        static CategoriasUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options;
        }

        public CategoriasUnitTestController()
        {
            //Criando referência ao mapeamento
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });
            mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);//Contexto

            //Se estivesse usando um BD vazio, usariamos a classe para alimentar.
            //DBUnitTestMockInitializer db = new DBUnitTestMockInitializer();
            //db.Seed(context);

            repository = new UnitOfWork(context);//Instância do contexto.
        }

        //Testes Unitários ===========================================================================

        //Testar Metodo GET
        [Fact]
        public void GetCategorias_Return_okResult()
        {
            //Arrange
            var controller = new CategoriasController(repository, null, mapper);

            //Act
            var data = controller.Get();

            //Assert
            Assert.IsType<List<CategoriaDTO>>(data.Value);
        }
    }
}
