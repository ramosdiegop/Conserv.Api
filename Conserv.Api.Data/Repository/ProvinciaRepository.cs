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

namespace Conserv.Api.Data.Repository
{
	public class ProvinciaRepository : IProvincia
	{
		private readonly DBContext _Conte;

		public ProvinciaRepository(DBContext conte)
		{
			_Conte = conte;
		}
		public async Task<PagedResponse<List<Provincia>>> PostAll(PaginationFilter filter)
		//public async Task<List<ProvinciaDto>> GetAll()
		{
			var validFilter = new PaginationFilter();
			validFilter.PageNumber = filter.PageNumber;
			validFilter.PageSize = filter.PageSize;
			List<Provincia> LProvincia = new List<Provincia>();
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
				var LProvincia1 = await _Conte.Provincia
										//.Include(t => t.Localidades)
										.OrderBy($"{Elorden} {LaForma}")
										.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
										.Take(validFilter.PageSize).ToListAsync();
			
				LProvincia = LProvincia1;
				totalRecords = _Conte.Provincia.Count();
			}
			else
			{
				string[] col = filter.Colum;
				string[] fil = filter.Filtro;
				var predicate = PredicateBuilder.New<Provincia>();

				string fpr, fde = "";
				int fid = 0;
				int cont = 0;
				foreach (string item in filter.Colum)
				{
					if (item.ToString().ToUpper() == "ID")
					{
						fid = int.Parse(filter.Filtro[cont].ToString());
						predicate = predicate.And(p => p.Id == fid);
					}
					if (item.ToString().ToUpper() == "CODIGOPROVINCIA")
					{
						fpr = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.CodigoProvincia.Contains(fpr));

					}
					if (item.ToString().ToUpper() == "DETALLE")
					{
						fde = filter.Filtro[cont].ToString();
						predicate = predicate.And(p => p.Detalle.Contains(fde));
					}

					cont++;
				}

				var LProvincia1 = await _Conte.Provincia
									//.Include(t => t.Localidades)
									.Where(predicate)
									.OrderBy($"{Elorden} {LaForma}")
									.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
									.Take(validFilter.PageSize).ToListAsync();
				//						.Include(i => i.Agencia)
				//						.ThenInclude(i => i.IdlocalidadNavigation).ToListAsync();
				LProvincia = LProvincia1;

				totalRecords = _Conte.Provincia.Where(predicate).Count();

			}


			/*List<ProvinciaDto> dtoProvincia = new List<ProvinciaDto>();
			foreach (var item in LProvincia)
			{
			 	List<ProvinciaDto> AProvincia = new List<ProvinciaDto>();
				foreach (var iteml in item.Agencia)
				{
					var UnaAgencia = new ProvinciaAgencia
					{
						Id = iteml.Id,
						Idagencia = iteml.Idagencia,
						Nombre = iteml.Nombre,
						Direccion = iteml.Direccion,
						Localidad = (iteml.IdlocalidadNavigation.CodigoPostal==null) ? " " : iteml.IdlocalidadNavigation.CodigoPostal.ToString() +"-"+ iteml.IdlocalidadNavigation.Detalle,						
						Titular = iteml.Titular,	
						Cuit = iteml.Cuit,
						Llave = iteml.Llave,
						Ok	=	iteml.Ok,
						Banco = iteml.Banco
					};
					AProvincia.Add(UnaAgencia);
				}

				var UnProvincia = new ProvinciaDto
				{
					Id = item.Id,
					Nombre = item.Nombre,
					Cuit = item.Cuit,
					ProvinciaConserv = AProvincia
				};
				dtoProvincia.Add(UnProvincia);

			}*/
			var dtoProvincia = LProvincia;
			var totalPages = decimal.ToInt32(totalRecords / validFilter.PageSize);
			var pagedReponse = new PagedResponse<List<Provincia>>(dtoProvincia, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords);


			return pagedReponse;


		}

		public async Task<Provincia> GetById(int Id)
		{
			var LProvi = await _Conte.Provincia.Where(w => w.Id == Id).FirstOrDefaultAsync();
			return LProvi;
		}

		public async Task<Provincia> Create(Provincia Provincia)
		{
			await _Conte.Provincia.AddAsync(Provincia);
			await _Conte.SaveChangesAsync();
			return Provincia;
		}

		public async Task<string> Delete(int id)
		{
			try
			{
				string error = "";
				var ElProvincia = _Conte.Provincia.Find(id);
				if (ElProvincia != null)
				{
					_Conte.Provincia.Remove(ElProvincia);
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


		public async Task<Provincia> Update(Provincia Provincia)
		{
			_Conte.Provincia.Update(Provincia);
			await _Conte.SaveChangesAsync();
			return Provincia;

		}

		async Task<List<Provincia>> IProvincia.GetAllSelect()
		{
			var LProvincia = await _Conte.Provincia.ToListAsync();

			return LProvincia;

		}
	}

}
