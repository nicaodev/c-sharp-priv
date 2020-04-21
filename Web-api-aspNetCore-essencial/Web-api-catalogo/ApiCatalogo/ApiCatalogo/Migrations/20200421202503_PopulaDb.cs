using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCatalogo.Migrations
{
    public partial class PopulaDb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias(Nome,ImagemUrl) Values ('Bebidas','https://www.bebidaonline.com.br')");
            mb.Sql("INSERT INTO Categorias(Nome,ImagemUrl) Values ('Lanches','https://www.ifood.com.br/')");
            mb.Sql("INSERT INTO Categorias(Nome,ImagemUrl) Values ('Sobremesas','http://stoniaice.com.br/')");

            mb.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Coca-Cola', 'Refrigerante Lata', 5.45, 'imagem-coca.jpg', 50, GETDATE(),(SELECT CategoriaId from Categorias where Nome = 'Bebidas'))");
            mb.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Hambuguer Triplo', 'Hamburguer Morte Lenta', 20.45, 'dogao.jpg', 100, GETDATE() ,(SELECT CategoriaId from Categorias where Nome = 'Lanches'))");
            mb.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) Values('Pudin', 'Melhor Pudin do Mundo', 8.45, 'pudim.jpg', 90, GETDATE(),(SELECT CategoriaId from Categorias where Nome = 'Sobremesas'))");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete From Categorias"); 
            mb.Sql("Delete From Produtos");
        }
    }
}
