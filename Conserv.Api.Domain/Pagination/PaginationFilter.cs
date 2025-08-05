using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Domain.Pagination
{
	public class PaginationFilter
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string[] Colum { get; set; }
		public string[] Filtro { get; set; }
		public string COrder { get; set; }
		public bool FAscend { get; set; }


		public PaginationFilter()
		{
			PageNumber = 1;
			PageSize = 20;
			Colum = null;
			Filtro = null;
			COrder = null;
			FAscend = true;
		}
		public PaginationFilter(int pageNumber, int pageSize, string[] colum, string[] filtro, string corder, bool forder)
		{
			PageNumber = pageNumber < 1 ? 1 : pageNumber;
			PageSize = pageSize > 20 ? 20 : pageSize;
			Colum = colum == null ? null : colum;
			Filtro = filtro == null ? null : filtro;
			COrder = corder == "" ? "" : corder;
			FAscend = forder == null ? true : forder;
		}
	}
}
