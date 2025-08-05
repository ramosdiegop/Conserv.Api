using Conserv.Api.Authentication;
using Conserv.Api.Data.Data;
using Conserv.Api.Data.Mapper;
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
	public class MenuController : ControllerBase
	{
		private readonly IMenu _IMenu;
		private readonly IMapping _mapping;
		public MenuController(IMenu Imenu,IMapping Imapp)
		{
			_IMenu = Imenu;
			_mapping = Imapp;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ItemDto>>> GetByUsuario([FromQuery] int idusuario)
		{
			try
			{
				var Lmenu = await _IMenu.GetByUsuario(idusuario);
				var i_menu = _mapping.MappMenu(Lmenu);
				return Ok(i_menu);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}

		[HttpGet("{idmenu}")]
		public async Task<ActionResult<Menu>> GetById(int idmenu)
		{
			try
			{
				var Lmenu = await _IMenu.GetById(idmenu);				
				return Ok(Lmenu);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPost("usuario/{idusuario}")]
		public async Task<ActionResult<IEnumerable<MenuUsuario>>> PostAllUsuario(int idusuario)

		{
			var usuario = idusuario;
			try
			{
				if ((usuario == 0))
				{
					var Lmenu = await _IMenu.GetAllSelect(usuario);
					//var i_menu = _mapping.MappMenuCrud(Lmenu);
					return Ok(Lmenu);
				}
				else
				{
					var Lmenu = await _IMenu.GetAllSelect(usuario);
					//var i_menu = _mapping.MappMenuCrud(Lmenu);
					return Ok(Lmenu);
				}


			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}



		// GETAll: api Lista usuario/
		[HttpPost]
		public async Task<ActionResult<PagedResponse<List<Menu>>>> PostAll([FromBody] PaginationFilter filter)
		{
			try
			{
				var Lmenu = await _IMenu.PostAll(filter);
				return Ok(Lmenu);

			}
			catch (Exception e)
			{
				return NotFound("Error: " + e.Message);
			}

		}



		[HttpPut]
		public async Task<ActionResult<Menu>> Put(Menu menu)
		{

			try
			{
				var Lmenu = await _IMenu.Update(menu);
				return Ok(Lmenu);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}

		}

		[HttpPost("{id}")]
		public async Task<ActionResult<Menu>> Post(Menu menu)
		{
			Menu Elmenu;
			try
			{
				Elmenu = await _IMenu.Create(menu);
				return Ok(Elmenu);
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
				string resultado = await _IMenu.Delete(id);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}

		/*Menu Usuario  Accesos*/
		[HttpPost("acceso/")]
		public async Task<ActionResult<List<MenuUsuario>>> PostAccesos([FromBody] List<MenuUsuario> acceso)
		{
			List<MenuUsuario> elacceso;
			try
			{
				elacceso = await _IMenu.CreateAcceso(acceso);
				return Ok(elacceso);
			}
			catch (Exception e)
			{

				return NotFound("Error: " + e.Message);
			}


		}

		[HttpDelete("acceso")]
		public async Task<ActionResult> DeleteAcceso([FromBody]  List<MenuUsuario> acceso)
		{
			try
			{
				string resultado = await _IMenu.DeleteAcceso( acceso);
				return Ok(resultado);

			}
			catch (Exception e)
			{
				return NotFound(e.Message);

			}

		}

	}

}
