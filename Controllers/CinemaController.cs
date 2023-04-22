using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;
    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]

    public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemadto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemadto);
        _context.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemaPorId), new { id = cinema.Id }, cinema);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDto> RecuperaCinemas([FromQuery] int? enderecoId = null)
    {
        if (enderecoId == null)
        {
          return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
        }
          return _mapper.Map<List<ReadCinemaDto>>(
                 _context.Cinemas.FromSqlRaw($"SELECT Id,Nome,EnderecoId FROM cinemas where cinemas.EnderecoId = {enderecoId}").ToList()
                  );   
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaCinemaPorId(int id) 
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) 
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ReadCinemaDto>(cinema));
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCinema( int id , [FromBody] UpdateCinemaDto cinemaDto) 
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) 
        {
            return NotFound();
        }
        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaCinema(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null)
        {
            return NotFound();
        }
        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }


}