using Conserv.Api.Data.Context;
using Conserv.Api.Data.Data;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Dtos;
using Conserv.Api.Domain.Pagination;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace Conserv.Api.Data.Repository
{
	public class MenuRepository : IMenu
	{
		private readonly DBContext _Conte;

		public MenuRepository(DBContext conte)
		{
			_Conte = conte;
		}


		public async Task<List<MenuDto>> GetByUsuario(int idusuario)
		{
			/*var Lmenu = await _Conte.Menues.ToListAsync();*/
			List<MenuDto> dtoMenu = new List<MenuDto>();

			if (idusuario != 0)
			{
				var Lmenu = await _Conte.MenuUsuarios
										.Where(w => w.Usuario_id == idusuario)
										.Include(i => i.Menues)
										.ToListAsync();

				
				foreach (var item in Lmenu)
				{
					var unmenu = new MenuDto
					{
						Id = item.Menu_id,
						Nombre = item.Menues.Nombre,
						Esmenu = item.Menues.Esmenu,
						EsSubmenu = item.Menues.EsSubmenu,
						Eslink = item.Menues.Eslink,
						Idmenu = item.Menues.Idmenu,
						Idsubmenu = item.Menues.Idsubmenu,
						link = item.Menues.link,
						Ejecutable = item.Menues.Ejecutable,

					};
					dtoMenu.Add(unmenu);

				}
			}
			else
			{
				var Lmenu = await _Conte.Menues
										.ToListAsync();

				foreach (var item in Lmenu)
				{
					var unmenu = new MenuDto
					{
						Id = item.Id,
						Nombre = item.Nombre,
						Esmenu = item.Esmenu,
						EsSubmenu = item.EsSubmenu,
						Eslink = item.Eslink,
						Idmenu = item.Idmenu,
						Idsubmenu = item.Idsubmenu,
						link = item.link,
						Ejecutable = item.Ejecutable,

					};
					dtoMenu.Add(unmenu);

				}

			}


			return dtoMenu;

		}


		public async Task<Menu> Create(Menu menu)
		{
			await _Conte.Menues.AddAsync(menu);
			await _Conte.SaveChangesAsync();
			return menu;
		}

		public async Task<string> Delete(int id)
		{
			string error = "Registro eliminado Correctamente ";
			var ElMenu = _Conte.Menues.Find(id);
			if (ElMenu != null)
			{
				_Conte.Menues.Remove(ElMenu);
				await _Conte.SaveChangesAsync();
			}
			else error = "No se encontro el registro";

			return error;
		}

		public async Task<List<MenuUsuario>> GetAllSelect(int idusuario)
		{
			List<Menu> Lmenu = new List<Menu>();
			List<MenuUsuario> usuariomenu = new List<MenuUsuario>();
			if (idusuario == 0) { 
				//Lmenu = await _Conte.Menues.ToListAsync();
				//usuariomenu = await _Conte.MenuUsuarios.ToListAsync();
			}
			else
			{
				usuariomenu = await _Conte.MenuUsuarios
									.Where(w => w.Usuario_id == idusuario)
									//.Include(i => i.Menues)
									.ToListAsync();

				/*foreach (var item in usuariomenu)
				{					
					Lmenu.Add(item.Menues);
				}*/
				

			}
				

			return usuariomenu;
		}

		public async Task<Menu> GetById(int Id)
		{
			Menu Lmenu = new Menu();
			Lmenu = await _Conte.Menues.Where(w => w.Id == Id).FirstOrDefaultAsync();

			return Lmenu;
		}


		public async Task<PagedResponse<List<Menu>>> PostAll(PaginationFilter filter)
		{
			var validFilter = new PaginationFilter();
			validFilter.PageNumber = filter.PageNumber;
			validFilter.PageSize = filter.PageSize;
			List<Menu> Lmenu = new List<Menu>();
			int totalRecords = 0;
			string Elorden = filter.COrder;
			string LaForma = filter.FAscend == true ? "ascending" : "descending";


			var Verify = new FunctionRepository();
			var listaarray = Verify.VerifyArrayFilter(filter.Filtro, filter.Colum);
			filter.Filtro = listaarray[0];
			filter.Colum = listaarray[1];

			if (string.IsNullOrEmpty(filter.COrder))
				Elorden = "Id";

			if (filter.Colum.Count() == 0)
			{
				var Lmenu1 = await _Conte.Menues
										.OrderBy($"{Elorden} {LaForma}")
										.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
										.Take(validFilter.PageSize).ToListAsync();

				Lmenu = Lmenu1;
				totalRecords = _Conte.Menues.Count();
			}
			else
			{
				string[] col = filter.Colum;
				string[] fil = filter.Filtro;
				var predicate = PredicateBuilder.New<Menu>();

				string fno, fcu = "";
				int fid = 0;
				int cont = 0;
				foreach (string item in filter.Colum)
				{
					if (item.ToString().ToUpper() == "ID")
					{
						fid = int.Parse(filter.Filtro[cont].ToString());
						predicate = predicate.And(p => p.Id == fid);
					}
					if (item.ToString().ToUpper() == "NOMBRE")
					{
						fno = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Nombre.Contains(fno));

					}
					if (item.ToString().ToUpper() == "LINK")
					{
						fcu = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.link.Contains(fcu));
					}
					if (item.ToString().ToUpper() == "ESMENU")
					{
						fcu = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Esmenu==fcu);
					}
					if (item.ToString().ToUpper() == "ESSUBMENU")
					{
						fcu = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.EsSubmenu==fcu);
					}
					if (item.ToString().ToUpper() == "ESLINK")
					{
						fcu = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Eslink==fcu);
					}
					if (item.ToString().ToUpper() == "EJECUTABLE")
					{
						fcu = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Ejecutable==fcu);
					}

					cont++;
				}

				var Lmenu1 = await _Conte.Menues.Where(predicate)
									.OrderBy($"{Elorden} {LaForma}")
									.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
									.Take(validFilter.PageSize).ToListAsync();

				Lmenu = Lmenu1;


				totalRecords = _Conte.Menues.Where(predicate).Count();

			}

			var totalPages = decimal.ToInt32(totalRecords / validFilter.PageSize);
			var pagedReponse = new PagedResponse<List<Menu>>(Lmenu, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords);

			return pagedReponse;


		}

		public async Task<Menu> Update(Menu menu)
		{
			_Conte.Menues.Update(menu);
			await _Conte.SaveChangesAsync();
			return menu;
		}

		public async Task<List<MenuUsuario>> CreateAcceso(List<MenuUsuario> acceso)
		{
			await _Conte.MenuUsuarios.AddRangeAsync(acceso);
			await _Conte.SaveChangesAsync();
			return acceso;
		}

		public async Task<List<MenuUsuario>> UpdateAcceso(List<MenuUsuario> acceso)
		{
			_Conte.MenuUsuarios.UpdateRange(acceso);
			await _Conte.SaveChangesAsync();
			return acceso;

		}

		public async Task<string> DeleteAcceso(List<MenuUsuario> acceso)
		{
			string error = "Registro eliminado Correctamente ";
			//var ElMenuusaurio = _Conte.MenuUsuarios.Find(acceso);
			var ElMenuusaurio = acceso;
			if (ElMenuusaurio != null)
			{
				_Conte.MenuUsuarios.RemoveRange(ElMenuusaurio);
				await _Conte.SaveChangesAsync();
			}
			else error = "No se encontro el registro";

			return error;

		}
	}
}
