using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Data
{
	[Table("equipo")]
	public class Equipo
	{
		[Key]
		public int Id { get; set; }
		public int Codigo_Equipo { get; set; }
		public string Modelo { get; set; }
		[MaxLength(20)]
		public string? Patente { get; set; }
		public int Anio { get; set; }
		public string? Numero_Motor { get; set; }
		public string? Numero_Chasis { get; set; }
		public string? Numero_Serie { get; set; }
		public Double Alcance { get; set; }
		public string? Capacidades { get; set; }
		public string? Tiempo_Minimo { get; set; }
		public Double Costo_Hora { get; set; }
		[MaxLength(2)]
		public string? Factura_General { get; set; }
		public int Estado { get; set; }
		[MaxLength(2)]
		public string? Dgastos { get; set; }
		public int Ver_Agenda { get; set; }
		public DateTime Created_Date { get; set; }
		public int Created_User { get; set; }
		public DateTime? Modified_Date { get; set; }
		public int? Modified_User { get; set; }


	}
}
