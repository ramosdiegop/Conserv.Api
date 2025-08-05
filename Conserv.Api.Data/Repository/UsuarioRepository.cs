using Conserv.Api.Data.Data;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Pagination;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conserv.Api.Data.Context;
using Conserv.Api.Domain.Dtos;

namespace Conserv.Api.Data.Repository
{
	public class UsuarioRepository : IUsuario
	{
		private readonly DBContext _Conte;

		public UsuarioRepository(DBContext conte)
		{
			_Conte = conte;
		}

		public async Task<PagedResponse<List<UsuarioDto>>> PostAll(PaginationFilter filter)
		{
			var validFilter = new PaginationFilter();
			validFilter.PageNumber = filter.PageNumber;
			validFilter.PageSize = filter.PageSize;
			List<Usuario> Lusuario = new List<Usuario>();
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
				var Lusuario1 = await _Conte.Usuario
										.OrderBy($"{Elorden} {LaForma}")
										.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
										.Take(validFilter.PageSize).ToListAsync();

				Lusuario = Lusuario1;
				totalRecords = _Conte.Usuario.Count();
			}
			else
			{
				string[] col = filter.Colum;
				string[] fil = filter.Filtro;
				var predicate = PredicateBuilder.New<Usuario>();

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
					if (item.ToString().ToUpper() == "NOMBRE_USUARIO")
					{
						fcu = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Nombre_Usuario.Contains(fcu));
					}

					cont++;
				}

				var Lusuario1 = await _Conte.Usuario.Where(predicate)
									.OrderBy($"{Elorden} {LaForma}")
									.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
									.Take(validFilter.PageSize).ToListAsync();

				Lusuario = Lusuario1;


				totalRecords = _Conte.Usuario.Where(predicate).Count();

			}
 

			List<UsuarioDto> dtoUsuario = new List<UsuarioDto>();

			foreach (var item in Lusuario)
			{				
				var UnUsuario = new UsuarioDto
				{
					Id = item.Id,
					Nombre = item.Nombre,
					Nombre_Usuario = item.Nombre_Usuario,
					Password = item.Password,
					Created_Date = item.Created_Date,
					Created_User = item.Created_User,
					Modified_Date = item.Modified_Date,
					Modified_User = item.Modified_User

				};

				dtoUsuario.Add(UnUsuario);
			}

			var totalPages = decimal.ToInt32(totalRecords / validFilter.PageSize);
			var pagedReponse = new PagedResponse<List<UsuarioDto>>(dtoUsuario, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords);



			return pagedReponse;


		}

		public Task<Usuario> GetById(int Id)
		{
			throw new NotImplementedException();
		}

		public async Task<Usuario> Create(Usuario usuario)
		{	
			await _Conte.Usuario.AddAsync(usuario);
			await _Conte.SaveChangesAsync();
			return usuario;
		}

		public async Task<string> Delete(int id)
		{
			string error = "Registro eliminado Correctamente ";
			var Elusuario = _Conte.Usuario.Find(id);
			if (Elusuario != null)
			{
				_Conte.Usuario.Remove(Elusuario);
				await _Conte.SaveChangesAsync();
			}
			else error = "No se encontro el registro";

			return error;
		}


		public async Task<Usuario> Update(Usuario usuario)
		{
			_Conte.Usuario.Update(usuario);
			await _Conte.SaveChangesAsync();
			return usuario;
		}

		public async Task<List<Usuario>> GetAllSelect()
		{
			var Lusuario = await _Conte.Usuario.ToListAsync();
			return Lusuario;
				
		}

		public async Task<UsuarioDto> GetByUsuario(string usuario)
		{
			var Lusuario = await _Conte.Usuario.Where(w => w.Nombre_Usuario==usuario)
									.FirstOrDefaultAsync();

			UsuarioDto dtoUsuario = new UsuarioDto();
			if (!(Lusuario == null))
			{
				dtoUsuario.Id = Lusuario.Id;
				dtoUsuario.Nombre = Lusuario.Nombre;
				dtoUsuario.Nombre_Usuario = usuario;
				dtoUsuario.Password = Lusuario.Password;
				dtoUsuario.Created_Date = Lusuario.Created_Date;
				dtoUsuario.Created_User = Lusuario.Created_User;
				dtoUsuario.Modified_Date = Lusuario.Modified_Date;
				dtoUsuario.Modified_User = Lusuario.Modified_User;


			}
			

			return dtoUsuario;
		}
	}

}

