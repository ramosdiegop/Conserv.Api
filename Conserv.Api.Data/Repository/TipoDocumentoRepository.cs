using Conserv.Api.Data.Context;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Data.Data;
using Conserv.Api.Domain.Pagination;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Conserv.Api.Data.Repository
{
	public class TipoDocumentoRepository : ITipoDocumento
	{
		private readonly DBContext _Conte;

		public TipoDocumentoRepository(DBContext conte)
		{
			_Conte = conte;
		}

		public async Task<PagedResponse<List<TipoDocumento>>> PostAll(PaginationFilter filter)
		{
			var validFilter = new PaginationFilter();
			validFilter.PageNumber = filter.PageNumber;
			validFilter.PageSize = filter.PageSize;
			List<TipoDocumento> Ltipo = new List<TipoDocumento>();
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
				var Ltipo1 = await _Conte.TipoDocumento
										.OrderBy($"{Elorden} {LaForma}")
										.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
										.Take(validFilter.PageSize).ToListAsync();
				//	.Include(i => i.Agencia)
				//	.ThenInclude(i => i.IdlocalidadNavigation).ToListAsync();
				Ltipo = Ltipo1;
				totalRecords = _Conte.TipoDocumento.Count();
			}
			else
			{
				string[] col = filter.Colum;
				string[] fil = filter.Filtro;
				var predicate = PredicateBuilder.New<TipoDocumento>();

				string fno, fco = "";
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
					if (item.ToString().ToUpper() == "CODIGO")
					{
						fco = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Codigo.Contains(fco));

					}

					cont++;
				}

				var Ltipo1 = await _Conte.TipoDocumento.Where(predicate)
									.OrderBy($"{Elorden} {LaForma}")
									.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
									.Take(validFilter.PageSize).ToListAsync();


				Ltipo = Ltipo1;

				totalRecords = _Conte.TipoDocumento.Where(predicate).Count();

			}

			var totalPages = decimal.ToInt32(totalRecords / validFilter.PageSize);
			var pagedReponse = new PagedResponse<List<TipoDocumento>>(Ltipo, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords);


			return pagedReponse;


		}

		public Task<TipoDocumento> GetById(int Id)
		{
			throw new NotImplementedException();
		}

		public async Task<TipoDocumento> Create(TipoDocumento tipo)
		{
			await _Conte.TipoDocumento.AddAsync(tipo);
			await _Conte.SaveChangesAsync();
			return tipo;
		}

		public async Task<string> Delete(int id)
		{
			string error = "Registro eliminado Correctamente ";
			var ElTipo = _Conte.TipoDocumento.Find(id);
			if (ElTipo != null)
			{
				_Conte.TipoDocumento.Remove(ElTipo);
				await _Conte.SaveChangesAsync();
			}
			else error = "No se encontro el registro";
			return error;

		}


		public async Task<TipoDocumento> Update(TipoDocumento tipo)
		{
			_Conte.TipoDocumento.Update(tipo);
			await _Conte.SaveChangesAsync();
			return tipo;

		}

		public async Task<List<TipoDocumento>> GetAllSelect()
		{
			var Ltipo = await _Conte.TipoDocumento.ToListAsync();

			return Ltipo;

		}


	}
}
