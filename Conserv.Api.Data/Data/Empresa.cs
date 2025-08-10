using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Data
{
	[Table("empresa")]
	public class Empresa
	{		
		[Key]
		public int Id { get; set; }
		public int Codigo_Empresa { get; set; }
		public string Nombre { get; set; }
		[MaxLength(20)]
		public string? Cuit { get; set; }
		[MaxLength(20)]
		public string? Telefono { get; set; }
		[ForeignKey("Localidad")]
		public int Localidad_Id { get; set; }
		public string? Domicilio { get; set; }
		[MaxLength(15)]
		public string? Abrevia { get; set; }
		public string? Logo { get; set; }
		public string? Email { get; set; }
		public string? Web { get; set; }
		public DateTime Created_Date { get; set; }
		public int Created_User { get; set; }
		public DateTime? Modified_Date { get; set; }
		public int? Modified_User { get; set; }


		public virtual Localidad? Localidad { get; set; }


	}
}
