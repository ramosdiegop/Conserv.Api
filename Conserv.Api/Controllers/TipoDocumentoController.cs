using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Conserv.Api;
using Conserv.Api.Authentication;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Pagination;
using Conserv.Api.Data.Data;

namespace Conserv.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authenticate]
	public class TipoDocumentoController : ControllerBase
	{
		private readonly ITipoDocumento _ITipo;
		public TipoDocumentoController(ITipoDocumento ITipo)
		{
			_ITipo = ITipo;
		}
		// GETAll: api Lista generos/
		[HttpPost]
		public async Task<ActionResult<PagedResponse<List<TipoDocumento>>>> PostAll([FromBody] PaginationFilter filter)
		{
			try
			{
				var Ltipo = await _ITipo.PostAll(filter);
				return Ok(Ltipo);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}


		[HttpGet("{select}")]
		public async Task<ActionResult<IEnumerable<TipoDocumento>>> GetAllSelect()
		{
			try
			{
				var Ltipo = await _ITipo.GetAllSelect();
				return Ok(Ltipo);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}



		[HttpPut]
		public async Task<ActionResult<TipoDocumento>> Put(TipoDocumento tipo)
		{

			try
			{
				var Ltipo = await _ITipo.Update(tipo);
				return Ok(Ltipo);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPost("{id}")]
		public async Task<ActionResult<TipoDocumento>> Post(TipoDocumento tipo)
		{
			TipoDocumento Ltipo;
			try
			{
				Ltipo = await _ITipo.Create(tipo);
				return Ok(Ltipo);
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
				string resultado = await _ITipo.Delete(id);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}



	}
}
