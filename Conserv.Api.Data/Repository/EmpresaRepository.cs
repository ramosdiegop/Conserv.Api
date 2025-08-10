using Conserv.Api.Data.Context;
using Conserv.Api.Data.Data;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Pagination;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Conserv.Api.Data.Repository
{
	public class EmpresaRepository : IEmpresa
	{

		private readonly DBContext _Conte;

		public EmpresaRepository(DBContext conte)
		{
			_Conte = conte;
		}

		public async Task<PagedResponse<List<Empresa>>> PostAll(PaginationFilter filter)
		{
			var validFilter = new PaginationFilter();
			validFilter.PageNumber = filter.PageNumber;
			validFilter.PageSize = filter.PageSize;
			List<Empresa> LEmpre = new List<Empresa>();
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
				var LEmpre1 = await _Conte.Empresa
								.Include(i => i.Localidad)	
								.OrderBy($"{Elorden} {LaForma}")
								.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
								.Take(validFilter.PageSize).ToListAsync();

				LEmpre = LEmpre1;
				totalRecords = _Conte.Empresa.Count();
			}
			else
			{
				string[] col = filter.Colum;
				string[] fil = filter.Filtro;
				var predicate = PredicateBuilder.New<Empresa>();

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
					if (item.ToString().ToUpper() == "NOMBRE")
					{
						fno = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Nombre.Contains(fno));

					}
					if (item.ToString().ToUpper() == "CODIGO_EMPRESA")
					{
						fid = int.Parse(filter.Filtro[cont].ToString());
						predicate = predicate.And(p => p.Codigo_Empresa == fid);

					}

					if (item.ToString().ToUpper() == "CUIT")
					{
						fno = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Cuit.Contains(fno));

					}

					cont++;
				}

				var LEmpre1 = await _Conte.Empresa.Where(predicate)
									.OrderBy($"{Elorden} {LaForma}")
									.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
									.Take(validFilter.PageSize).ToListAsync();


				LEmpre = LEmpre1;

				totalRecords = _Conte.Empresa.Where(predicate).Count();

			}

			var totalPages = decimal.ToInt32(totalRecords / validFilter.PageSize);
			var pagedReponse = new PagedResponse<List<Empresa>>(LEmpre, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords);


			return pagedReponse;
		}

		public async Task<Empresa> Update(Empresa empresa)
		{
			_Conte.Empresa.Update(empresa);
			await _Conte.SaveChangesAsync();
			return empresa;
		}

		public async Task<Empresa> Create(Empresa empresa)
		{
			await _Conte.Empresa.AddAsync(empresa);
			await _Conte.SaveChangesAsync();
			return empresa;
		}


		public async Task<string> Delete(int id)
		{
			string error = "Registro eliminado Correctamente ";
			var LaEmpresa = _Conte.Empresa.Find(id);
			if (LaEmpresa != null)
			{
				_Conte.Empresa.Remove(LaEmpresa);
				await _Conte.SaveChangesAsync();
			}
			else error = "No se encontro el registro";

			return error;
		}
		public async Task<Empresa> GetById(int Id)
		{
			Empresa LEmpresa = new Empresa();
			LEmpresa = await _Conte.Empresa.Where(w => w.Id == Id).FirstOrDefaultAsync();

			return LEmpresa;
		}

	}
}
