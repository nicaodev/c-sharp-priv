using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Domains
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }

        public ICollection<Produto> Produtos { get; set; } // Criando a propriedade de navegação. Relacionamento Categoria pode ter n produtos.
    }
}
