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
	public class EmpresaController : ControllerBase
	{
		private readonly IEmpresa _IEmpresa;
		public EmpresaController(IEmpresa IEmpre)
		{
			_IEmpresa = IEmpre;
		}
		// GETAll: api Lista generos/
		[HttpPost]
		public async Task<ActionResult<PagedResponse<List<Empresa>>>> PostAll([FromBody] PaginationFilter filter)
		{
			try
			{
				var LEmpresa = await _IEmpresa.PostAll(filter);
				return Ok(LEmpresa);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}



		[HttpPut]
		public async Task<ActionResult<Empresa>> Put(Empresa empresa)
		{

			try
			{
				var LaEmpresa = await _IEmpresa.Update(empresa);
				return Ok(LaEmpresa);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPost("{id}")]
		public async Task<ActionResult<Empresa>> Post(Empresa equipo)
		{
			Empresa LaEmpresa;
			try
			{
				LaEmpresa = await _IEmpresa.Create(equipo);
				return Ok(LaEmpresa);
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
				string resultado = await _IEmpresa.Delete(id);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}



	}
}
