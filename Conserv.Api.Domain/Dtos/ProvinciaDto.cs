using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Domain.Dtos
{
	public class ProvinciaDto
	{

		public int Id { get; set; }
		public string? CodigoProvincia { get; set; }
		public string? Detalle { get; set; }
		public List<LasLocalidades>? Localidades { get; set; }

	}

	public class LasLocalidades
	{
		public int Id { get; set; }
		public int? CodigoPostal { get; set; }
		public string? Detalle { get; set; }

	}
}
