using ApiCatalogo.Context;
using ApiCatalogo.Domains;
using ApiCatalogo.Repository.UnitOfWork;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        //Injeçao de dependencia nativa. Possivel pois setamos o AppDbContext -> UnitOfWork como servico na classe Startup configure services.
        private readonly IUnitOfWork _context;
        private readonly IConfiguration _configuration;
        public CategoriasController(IUnitOfWork contexto, IConfiguration config)
        {
            _context = contexto;
            _configuration = config;
        }
        [HttpGet("Autor")]
        public string GetAutor()
        {
            //Acessando propriedades do app.settings.json. Para demais niveis navegar via definicoes, ex: "ConnectionStrings:DefaultConnection"
            var autor = _configuration["autor"];
            return $"Autor: {autor}";
        }

        /***
         * Injetando as dependencias diretamente no metodo Action do controlador. Deixando disponivel apensar para o método e não do modo tradicional _contexto
         * que deixa disponivel para todos os métodos.
         */
        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            return _context.CategoriaRepository.Get().ToList();
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return _context.CategoriaRepository.GetCategoriasProdutos().ToList();
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var retorno = _context.CategoriaRepository.GetById(x => x.CategoriaId == id);

            if (retorno == null)
                return NotFound();

            return retorno;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            _context.CategoriaRepository.Add(categoria);
            _context.Commit();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _context.CategoriaRepository.Update(categoria);
            _context.Commit();
            return Ok();
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var retorno = _context.CategoriaRepository.GetById(x => x.CategoriaId == id);

            if (retorno == null)
                return NotFound();

            _context.CategoriaRepository.Delete(retorno);
            _context.Commit();
            return retorno;
        }
    }
}
