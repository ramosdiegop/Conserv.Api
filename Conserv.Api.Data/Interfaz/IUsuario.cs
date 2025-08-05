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
	public interface IUsuario
	{
		Task<PagedResponse<List<UsuarioDto>>> PostAll(PaginationFilter filter);
		Task<List<Usuario>> GetAllSelect();
		Task<Usuario> Create(Usuario usuario);
		Task<Usuario> Update(Usuario usuario);
		Task<string> Delete(int id);
		Task<Usuario> GetById(int Id);
		Task<UsuarioDto> GetByUsuario(string usuario);


	}
}
