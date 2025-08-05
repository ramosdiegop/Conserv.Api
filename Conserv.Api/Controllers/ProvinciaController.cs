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
	public class ProvinciaController : ControllerBase
	{
		private readonly IProvincia _IProvincia;
		public ProvinciaController(IProvincia IProv)
		{
			_IProvincia = IProv;
		}

		// GETAll: api Lista Provincias/
		[HttpPost]
		public async Task<ActionResult<PagedResponse<List<Provincia>>>> PostAll([FromBody] PaginationFilter filter)
		{
			try
			{
				var Lagente = await _IProvincia.PostAll(filter);
				return Ok(Lagente);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}



		[HttpPut]
		public async Task<ActionResult<Provincia>> Put(Provincia provi)
		{

			try
			{
				var LaProvi = await _IProvincia.Update(provi);
				return Ok(LaProvi);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPost("{id}")]
		public async Task<ActionResult<Provincia>> Post(Provincia provi)
		{
			Provincia LaProvi;
			try
			{
				LaProvi = await _IProvincia.Create(provi);
				return Ok(LaProvi);
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
				string resultado = await _IProvincia.Delete(id);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			try
			{
				var resultado = await _IProvincia.GetById(id);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}



	}
}
