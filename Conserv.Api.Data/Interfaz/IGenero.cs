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
	public interface IGenero
	{
		Task<PagedResponse<List<Genero>>> PostAll(PaginationFilter filter);
		Task<List<Genero>> GetAllSelect();
		Task<Genero> Create(Genero genero);
		Task<Genero> Update(Genero genero);
		Task<string> Delete(int id);
		Task<Genero> GetById(int Id);

	}
}
