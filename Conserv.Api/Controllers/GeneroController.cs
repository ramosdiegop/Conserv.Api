using Conserv.Api.Authentication;
using Conserv.Api.Data.Data;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conserv.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authenticate]
	public class GeneroController : ControllerBase
	{
		private readonly IGenero _IGenero;
		public GeneroController(IGenero IGene)
		{
			_IGenero = IGene;
		}
		// GETAll: api Lista generos/
		[HttpPost]
		public async Task<ActionResult<PagedResponse<List<Genero>>>> PostAll([FromBody] PaginationFilter filter)
		{
			try
			{
				var Lagente = await _IGenero.PostAll(filter);
				return Ok(Lagente);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}


		[HttpGet("{select}")]
		public async Task<ActionResult<IEnumerable<Genero>>> GetAllSelect()
		{
			try
			{
				var Lagente = await _IGenero.GetAllSelect();
				return Ok(Lagente);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}



		[HttpPut]
		public async Task<ActionResult<Genero>> Put(Genero genero)
		{

			try
			{
				var Elagente = await _IGenero.Update(genero);
				return Ok(Elagente);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPost("{id}")]
		public async Task<ActionResult<Genero>> Post(Genero genero)
		{
			Genero Elgenero;
			try
			{
				Elgenero = await _IGenero.Create(genero);
				return Ok(Elgenero);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}


		}


		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				string resultado = await _IGenero.Delete(id);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}



	}
}
