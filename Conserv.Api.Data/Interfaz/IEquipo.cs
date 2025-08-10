using Conserv.Api.Data.Data;
using Conserv.Api.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Interfaz
{
	public interface IEquipo
	{
		Task<PagedResponse<List<Equipo>>> PostAll(PaginationFilter filter);
		//Task<List<MenuUsuario>> GetAllSelect(int idusuario);
		Task<Equipo> Create(Equipo equipo);
		Task<Equipo> Update(Equipo equipo);
		Task<string> Delete(int id);
		Task<Equipo> GetById(int Id);

	}
}
