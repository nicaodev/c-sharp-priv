﻿using ApiCatalogo.Context;
using ApiCatalogo.Domains;
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
    public class ProdutosController : ControllerBase // Herdando desta classe que somente contém propriedades para API. E não para Views.
    {
        //Injeçao de dependencia nativa. Possivel pois setamos o AppDbContext como servico na classe Startup configure services.
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext contexto)
        {
            _context = contexto;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return _context.Produtos.AsNoTracking().ToList(); //AsNoTracking() desabilita o mapeamento do objeto para aumentar perfomance já que nao iremos altera-lo. Somente buscas.
        }
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var retorno = _context.Produtos.AsNoTracking().FirstOrDefault(x => x.ProdutoId == id);

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

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            //Usando p decorator ApiController, esta validação já é feita automaticamente.

            if (id != produto.ProdutoId)
                return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var retorno = _context.Produtos.FirstOrDefault(r => r.ProdutoId == id);

            if (retorno == null)
                return NotFound();

            _context.Produtos.Remove(retorno);
            _context.SaveChanges();
            return retorno;
        }
    }
}