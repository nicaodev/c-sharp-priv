using ApiCatalogo.Context;
using ApiCatalogo.Domains;
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
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public CategoriasController(AppDbContext contexto, IConfiguration config)
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
            return _context.Categorias.AsNoTracking().ToList();
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {
            return await _context.Categorias.Include(x => x.Produtos).ToListAsync();
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var retorno = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (retorno == null)
                return NotFound();

            return retorno;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var retorno = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (retorno == null)
                return NotFound();

            _context.Categorias.Remove(retorno);
            _context.SaveChanges();
            return retorno;
        }
    }
}
