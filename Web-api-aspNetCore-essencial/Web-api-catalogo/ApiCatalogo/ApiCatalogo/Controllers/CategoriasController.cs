using ApiCatalogo.Context;
using ApiCatalogo.Domains;
using ApiCatalogo.DTOs;
using ApiCatalogo.Repository.UnitOfWork;
using ApiCatalogo.Services;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategoriasController(IUnitOfWork contexto, IConfiguration config, IMapper mapper)
        {
            _context = contexto;
            _configuration = config;
            _mapper = mapper;
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
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _context.CategoriaRepository.Get().ToList();
            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
        {
            var categorias = _context.CategoriaRepository.GetCategoriasProdutos().ToList();
            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _context.CategoriaRepository.GetById(x => x.CategoriaId == id);

            if (categoria == null)
                return NotFound();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDTO;
        }
        [HttpPost]
        public ActionResult Post([FromBody] CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _context.CategoriaRepository.Add(categoria);
            _context.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoriaDTO);
        }
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
                return BadRequest();

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _context.CategoriaRepository.Update(categoria);
            _context.Commit();
            return Ok();
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _context.CategoriaRepository.GetById(x => x.CategoriaId == id);

            if (categoria == null)
                return NotFound();

            _context.CategoriaRepository.Delete(categoria);
            _context.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDTO;
        }
    }
}
