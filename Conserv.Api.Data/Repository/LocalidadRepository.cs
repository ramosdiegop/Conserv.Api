using Conserv.Api.Data.Data;
using Conserv.Api.Data.Interfaz;
using Conserv.Api.Domain.Dtos;
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

namespace Conserv.Api.Data.Repository
{
	public class LocalidadRepository : ILocalidad
	{
		private DBContext _Conte;

		public LocalidadRepository(DBContext conte)
		{
			_Conte = conte;
		}


		public async Task<PagedResponse<List<LocalidadDto>>> PostAll(PaginationFilter filter)
		{
			var validFilter = new PaginationFilter();
			validFilter.PageNumber = filter.PageNumber;
			validFilter.PageSize = filter.PageSize;
			List<Localidad> LLocalidad = new List<Localidad>();
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
				var LLocalidad1 = await _Conte.Localidad
										.Include(t => t.Provincia)
										.OrderBy($"{Elorden} {LaForma}")
										.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
										.Take(validFilter.PageSize).ToListAsync();
				LLocalidad = LLocalidad1;
				totalRecords = _Conte.Localidad.Count();
			}
			else
			{
				string[] col = filter.Colum;
				string[] fil = filter.Filtro;
				var predicate = PredicateBuilder.New<Localidad>();

				string fde, fpr = "";
				int fid, fco, fidprov = 0;
				int cont = 0;
				foreach (string item in filter.Colum)
				{
					if (item.ToString().ToUpper() == "ID")
					{
						fid = int.Parse(filter.Filtro[cont].ToString());
						predicate = predicate.And(p => p.Id == fid);
					}
					if (item.ToString().ToUpper() == "CODIGOPOSTAL")
					{
						fco = int.Parse(filter.Filtro[cont].ToString());
						predicate = predicate.And(p => p.CodigoPostal == fco);

					}
					if (item.ToString().ToUpper() == "DETALLE")
					{
						fde = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Detalle.Contains(fde));
					}

					if (item.ToString().ToUpper() == "PROVINCIA")
					{
						fpr = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Provincia.Detalle.Contains(fpr));
					}

					if (item.ToString().ToUpper() == "IDPROVINCIA")
					{
						fidprov = int.Parse(filter.Filtro[cont].ToString());
						predicate = predicate.And(p => p.Idprovincia == fidprov);
					}

					cont++;
				}

				var LLocalidad1 = await _Conte.Localidad
									.Include(t => t.Provincia)
									.Where(predicate)
									.OrderBy($"{Elorden} {LaForma}")
									.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
									.Take(validFilter.PageSize).ToListAsync();

				LLocalidad = LLocalidad1;

				totalRecords = _Conte.Localidad.Where(predicate).Count();

			}


			List<LocalidadDto> dtoLocalidad = new List<LocalidadDto>();
			foreach (var item in LLocalidad)
			{
				var UnaLocaliad = new LocalidadDto
				{
					Id = item.Id,
					Detalle = item.Detalle,
					CodigoPostal = item.CodigoPostal,
					Idprovincia = item.Idprovincia,
					Provincia = (item.Provincia == null) ? "" : item.Provincia.Detalle,
					Created_Date = item.Created_Date,
					Created_User = item.Created_User,
					Modified_Date = item.Modified_Date,
					Modified_User = item.Modified_User

				};
				dtoLocalidad.Add(UnaLocaliad);

			}

			var totalPages = decimal.ToInt32(totalRecords / validFilter.PageSize);
			var pagedReponse = new PagedResponse<List<LocalidadDto>>(dtoLocalidad, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords);


			return pagedReponse;


		}

		public Task<LocalidadDto> GetById(int Id)
		{
			throw new NotImplementedException();
		}

		public async Task<Localidad> Update(Localidad localidad)
		{
			_Conte.Localidad.Update(localidad);
			await _Conte.SaveChangesAsync();
			return localidad;
		}

		public async Task<Localidad> Create(Localidad localidad)
		{
			await _Conte.Localidad.AddAsync(localidad);
			await _Conte.SaveChangesAsync();
			return localidad;
		}

		public async Task<string> Delete(int id)
		{
			try
			{
				string error = "";
				var LaLocalidad = _Conte.Localidad.Find(id);
				if (LaLocalidad != null)
				{
					_Conte.Localidad.Remove(LaLocalidad);
					await _Conte.SaveChangesAsync();
				}
				else error = "No se encontro el registro";
				return error;
			}
			catch (Exception e)
			{

				return e.Message;
			}
		}



	}
}
