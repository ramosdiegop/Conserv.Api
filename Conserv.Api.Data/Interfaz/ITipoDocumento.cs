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
	public interface ITipoDocumento
	{
		Task<PagedResponse<List<TipoDocumento>>> PostAll(PaginationFilter filter);
		Task<List<TipoDocumento>> GetAllSelect();
		Task<TipoDocumento> Create(TipoDocumento tipodoc);
		Task<TipoDocumento> Update(TipoDocumento tipodoc);
		Task<string> Delete(int id);
		Task<TipoDocumento> GetById(int Id);

	}
}
