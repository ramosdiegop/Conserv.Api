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
	public class EquipoController : ControllerBase
	{
		private readonly IEquipo _IEquipo;
		public EquipoController(IEquipo IEquipo)
		{
			_IEquipo = IEquipo;
		}
		// GETAll: api Lista generos/
		[HttpPost]
		public async Task<ActionResult<PagedResponse<List<Equipo>>>> PostAll([FromBody] PaginationFilter filter)
		{
			try
			{
				var ElEquipo = await _IEquipo.PostAll(filter);
				return Ok(ElEquipo);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}



		[HttpPut]
		public async Task<ActionResult<Equipo>> Put(Equipo equipo)
		{

			try
			{
				var ElEquipo = await _IEquipo.Update(equipo);
				return Ok(ElEquipo);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPost("{id}")]
		public async Task<ActionResult<Equipo>> Post(Equipo equipo)
		{
			Equipo ElEquipo;
			try
			{
				ElEquipo = await _IEquipo.Create(equipo);
				return Ok(ElEquipo);
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
				string resultado = await _IEquipo.Delete(id);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}



	}
}
