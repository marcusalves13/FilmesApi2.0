using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SessaoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public SessaoController(FilmeContext context, IMapper mapper)
        {
            _mapper= mapper;
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaSessao([FromBody] CreateSessaoDto sessaoDto) 
        {
            Sessao newSessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Add(newSessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaSessaoPorId), new { filmeId = newSessao.FilmeId , cinemaId = newSessao.CinemaId}, newSessao);
        }

        [HttpGet]

        public IEnumerable<ReadSessaoDto> RecuperaSessoes()
        {
            return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes.ToList());
        }

        [HttpGet("{filmeId}/{CinemaId}")]
        public IActionResult RecuperaSessaoPorId(int filmeId, int cinemaId) 
        {
            var sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
            if (sessao == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ReadSessaoDto>(sessao));
        }
        
    }
}
