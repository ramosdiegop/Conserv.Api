using Conserv.Api.Data.Data;
using Conserv.Api.Domain.Dtos;
using Conserv.Api.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Interfaz
{
	public interface IMenu
	{
		Task<PagedResponse<List<Menu>>> PostAll(PaginationFilter filter);
		Task<List<MenuUsuario>> GetAllSelect(int idusuario);
		Task<Menu> Create(Menu menu);
		Task<Menu> Update(Menu menu);
		Task<string> Delete(int id);
		Task<Menu> GetById(int Id);
		Task<List<MenuDto>> GetByUsuario(int idusuario);
		Task<List<MenuUsuario>> CreateAcceso(List<MenuUsuario> acceso);
		Task<List<MenuUsuario>> UpdateAcceso(List<MenuUsuario> acceso);
		Task<string> DeleteAcceso(List<MenuUsuario> acceso);


	}
}
