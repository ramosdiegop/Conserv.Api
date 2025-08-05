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
	public interface ILocalidad
	{
		Task<PagedResponse<List<LocalidadDto>>> PostAll(PaginationFilter filter);
		Task<Localidad> Create(Localidad localidad);
		Task<Localidad> Update(Localidad localidad);
		Task<string> Delete(int id);
		Task<LocalidadDto> GetById(int Id);

	}
}
