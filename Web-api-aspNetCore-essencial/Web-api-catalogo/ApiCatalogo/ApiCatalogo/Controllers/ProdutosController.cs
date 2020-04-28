using ApiCatalogo.Context;
using ApiCatalogo.Domains;
using ApiCatalogo.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        //Injeçao de dependencia nativa. Possivel pois setamos o AppDbContext -> UnitOfWork como servico na classe Startup configure services.
        private readonly IUnitOfWork _uof;
        public ProdutosController(IUnitOfWork contexto)
        {
            _uof = contexto;
        }
        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPreco()
        {
            return _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return _uof.ProdutoRepository.Get().ToList(); //AsNoTracking() desabilita o mapeamento do objeto para aumentar perfomance já que nao iremos altera-lo. Somente buscas.
        }
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            //throw new Exception("Testando ExceptionMiddlewareExtensions. Forçando um erro.");
            var retorno = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (retorno == null)
                return NotFound();

            return retorno;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            //Usando p decorator ApiController, esta validação já é feita automaticamente.

            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();

            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Produto> Delete(int id)
        {
            var retorno = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (retorno == null)
                return NotFound();

            _uof.ProdutoRepository.Delete(retorno);
            _uof.Commit();
            return retorno;
        }
    }
}
