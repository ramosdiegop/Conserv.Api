using Conserv.Api.Authentication;
using Conserv.Api.Data.Data;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Dtos;
using Conserv.Api.Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conserv.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authenticate]
	public class LocalidadController : ControllerBase
	{
		private ILocalidad _Iloca;
		public LocalidadController(ILocalidad iloca)
		{
			_Iloca = iloca;
		}

		[HttpPost]
		public async Task<ActionResult<PagedResponse<List<LocalidadDto>>>> PostAll([FromBody] PaginationFilter filter)
		{
			try
			{
				var Lagente = await _Iloca.PostAll(filter);
				return Ok(Lagente);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPut]
		public async Task<ActionResult<Localidad>> Put(Localidad loca)
		{

			try
			{
				var Laloca = await _Iloca.Update(loca);
				return Ok(Laloca);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPost("{id}")]
		public async Task<ActionResult<Localidad>> Post(Localidad loca)
		{
			try
			{
				var Laloca = await _Iloca.Create(loca);
				return Ok(Laloca);
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
				string resultado = await _Iloca.Delete(id);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}

	}
}
