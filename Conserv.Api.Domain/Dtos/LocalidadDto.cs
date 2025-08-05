using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Domain.Dtos
{
	public class LocalidadDto
	{
		public int Id { get; set; }
		public int? CodigoPostal { get; set; }
		public string? Detalle { get; set; }
		public int? Idprovincia { get; set; }
		public string? Provincia { get; set; }
		public DateTime Created_Date { get; set; }
		public int Created_User { get; set; }
		public DateTime? Modified_Date { get; set; }
		public int? Modified_User { get; set; }
	}
}
