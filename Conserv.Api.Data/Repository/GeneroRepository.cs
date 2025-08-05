using Conserv.Api.Data.Context;
using Conserv.Api.Data.Data;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Pagination;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace Conserv.Api.Data.Repository
{
	public class GeneroRepository : IGenero
	{
		private readonly DBContext _Conte;

		public GeneroRepository(DBContext conte)
		{
			_Conte = conte;
		}
		public async Task<PagedResponse<List<Genero>>> PostAll(PaginationFilter filter)
		{
			var validFilter = new PaginationFilter();
			validFilter.PageNumber = filter.PageNumber;
			validFilter.PageSize = filter.PageSize;
			List<Genero> Lgenero = new List<Genero>();
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
				var Lgenero1 = await _Conte.Genero
										.OrderBy($"{Elorden} {LaForma}")
										.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
										.Take(validFilter.PageSize).ToListAsync();
				//	.Include(i => i.Agencia)
				//	.ThenInclude(i => i.IdlocalidadNavigation).ToListAsync();
				Lgenero = Lgenero1;
				totalRecords = _Conte.Genero.Count();
			}
			else
			{
				string[] col = filter.Colum;
				string[] fil = filter.Filtro;
				var predicate = PredicateBuilder.New<Genero>();

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
					if (item.ToString().ToUpper() == "DETALLE")
					{
						fno = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Detalle.Contains(fno));

					}


					cont++;
				}

				var Lgenero1 = await _Conte.Genero.Where(predicate)
									.OrderBy($"{Elorden} {LaForma}")
									.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
									.Take(validFilter.PageSize).ToListAsync();
				//.Include(i => i.Agencia)
				//.ThenInclude(i => i.IdlocalidadNavigation).ToListAsync();

				Lgenero = Lgenero1;

				totalRecords = _Conte.Genero.Where(predicate).Count();

			}


			var totalPages = decimal.ToInt32(totalRecords / validFilter.PageSize);
			var pagedReponse = new PagedResponse<List<Genero>>(Lgenero, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords);


			return pagedReponse;


		}

		public Task<Genero> GetById(int Id)
		{
			throw new NotImplementedException();
		}

		public async Task<Genero> Create(Genero genero)
		{
			await _Conte.Genero.AddAsync(genero);
			await _Conte.SaveChangesAsync();
			return genero;
		}

		public async Task<string> Delete(int id)
		{
			string error = "Registro eliminado Correctamente ";
			var Elgenero = _Conte.Genero.Find(id);
			if (Elgenero != null)
			{
				_Conte.Genero.Remove(Elgenero);
				await _Conte.SaveChangesAsync();
			}
			else error = "No se encontro el registro";
			return error;

		}


		public async Task<Genero> Update(Genero genero)
		{
			_Conte.Genero.Update(genero);
			await _Conte.SaveChangesAsync();
			return genero;

		}

		public async Task<List<Genero>> GetAllSelect()
		{
			var Lgenero = await _Conte.Genero.ToListAsync();

			return Lgenero;

		}
	}
}
