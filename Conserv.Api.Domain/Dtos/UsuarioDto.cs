using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Domain.Dtos
{
	public class UsuarioDto
	{
		public int Id { get; set; }
		public string? Nombre_Usuario { get; set; }
		public string? Nombre { get; set; }
		public string? Password { get; set; }
		public DateTime Created_Date { get; set; }
		public int Created_User { get; set; }
		public DateTime? Modified_Date { get; set; }
		public int? Modified_User { get; set; }

	}



}
