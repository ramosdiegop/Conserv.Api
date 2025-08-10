using Conserv.Api.Data.Context;
using Conserv.Api.Data.Data;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Pagination;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Conserv.Api.Data.Repository
{
	public class EquipoRepository : IEquipo
	{

		private readonly DBContext _Conte;

		public EquipoRepository(DBContext conte)
		{
			_Conte = conte;
		}

		public async Task<PagedResponse<List<Equipo>>> PostAll(PaginationFilter filter)
		{
			var validFilter = new PaginationFilter();
			validFilter.PageNumber = filter.PageNumber;
			validFilter.PageSize = filter.PageSize;
			List<Equipo> LEquipo = new List<Equipo>();
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
				var LEquipo1 = await _Conte.Equipo
										.OrderBy($"{Elorden} {LaForma}")
										.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
										.Take(validFilter.PageSize).ToListAsync();

				LEquipo = LEquipo1;
				totalRecords = _Conte.Empresa.Count();
			}
			else
			{
				string[] col = filter.Colum;
				string[] fil = filter.Filtro;
				var predicate = PredicateBuilder.New<Equipo>();

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
					if (item.ToString().ToUpper() == "MODELO")
					{
						fno = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Modelo.Contains(fno));

					}
					if (item.ToString().ToUpper() == "CODIGO_EQUIPO")
					{
						fid = int.Parse(filter.Filtro[cont].ToString());
						predicate = predicate.And(p => p.Codigo_Equipo == fid);

					}

					if (item.ToString().ToUpper() == "PATENTE")
					{
						fno = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Patente.Contains(fno));

					}
					if (item.ToString().ToUpper() == "ANIO")
					{
						fid = int.Parse(filter.Filtro[cont].ToString());
						predicate = predicate.And(p => p.Anio == fid);
					}

					cont++;
				}

				var LEquipo1 = await _Conte.Equipo.Where(predicate)
									.OrderBy($"{Elorden} {LaForma}")
									.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
									.Take(validFilter.PageSize).ToListAsync();


				LEquipo = LEquipo1;

				totalRecords = _Conte.Equipo.Where(predicate).Count();

			}

			var totalPages = decimal.ToInt32(totalRecords / validFilter.PageSize);
			var pagedReponse = new PagedResponse<List<Equipo>>(LEquipo, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords);


			return pagedReponse;
		}

		public async Task<Equipo> Update(Equipo equipo)
		{
			_Conte.Equipo.Update(equipo);
			await _Conte.SaveChangesAsync();
			return equipo;
		}

		public async Task<Equipo> Create(Equipo equipo)
		{
			await _Conte.Equipo.AddAsync(equipo);
			await _Conte.SaveChangesAsync();
			return equipo;
		}


		public async Task<string> Delete(int id)
		{
			string error = "Registro eliminado Correctamente ";
			var ElEquipo = _Conte.Equipo.Find(id);
			if (ElEquipo != null)
			{
				_Conte.Equipo.Remove(ElEquipo);
				await _Conte.SaveChangesAsync();
			}
			else error = "No se encontro el registro";

			return error;
		}
		public async Task<Equipo> GetById(int Id)
		{
			Equipo ElEquipo = new Equipo();
			ElEquipo = await _Conte.Equipo.Where(w => w.Id == Id).FirstOrDefaultAsync();

			return ElEquipo;
		}
	}
}
