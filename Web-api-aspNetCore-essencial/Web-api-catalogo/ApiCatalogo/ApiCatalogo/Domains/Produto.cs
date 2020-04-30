using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Domains
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage ="O Nome do produto é obrigatório.")]
        [MaxLength(80)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A descrição do produto é obrigatório.")]
        [MaxLength(300)]
        public string Descricao { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName ="decimal(8,2)")]
        [Required(ErrorMessage = "O Preço do produto é obrigatório.")]
        public decimal Preco { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
