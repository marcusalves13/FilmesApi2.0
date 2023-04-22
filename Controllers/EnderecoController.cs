using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class EnderecoController : ControllerBase
{
	private FilmeContext _context;
	private IMapper _mapper;
	public EnderecoController(FilmeContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpPost]
	public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
	{
		Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
		_context.Enderecos.Add(endereco);
		_context.SaveChanges();
		return Ok(endereco);

	}

	[HttpGet]
	public IEnumerable<ReadEnderecoDto> RecuperaEnderecos() 
	{
		return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.ToList());
	}

	[HttpGet("{id}")]
	public IActionResult RecuperaEnderecoPorId(int id)
	{
		var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
		if(endereco == null) { return NotFound(); }
		return Ok(_mapper.Map<ReadEnderecoDto>(endereco));
	}

	[HttpPut("{id}")]
	public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto) 
	{
		var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
		if (endereco == null) { return NotFound(); }
		_mapper.Map(enderecoDto,endereco);
		_context.SaveChanges();
		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult DeletaEndereco(int id)
	{
		var endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
		if (endereco == null) { return NotFound(); }
		_context.Remove(endereco);
		_context.SaveChanges();
		return NoContent();
	}
}
