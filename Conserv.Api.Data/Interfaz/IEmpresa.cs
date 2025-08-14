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
	public interface IEmpresa
	{
		Task<PagedResponse<List<Empresa>>> PostAll(PaginationFilter filter);
		//Task<List<MenuUsuario>> GetAllSelect(int idusuario);
		Task<Empresa> Create(Empresa empresa);
		Task<Empresa> Update(Empresa empresa);
		Task<string> Delete(int id);
		Task<Empresa> GetById(int Id);
		

	}
}
