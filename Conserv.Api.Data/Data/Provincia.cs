using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Data
{
	[Table("provincia")]
	public class Provincia
	{
		[Key]
		public int Id { get; set; }
//		[Column("provincia")]
		public string CodigoProvincia { get; set; } = null!;
		public string Detalle { get; set; } = null!;
		public DateTime Created_Date { get; set; }
		public int Created_User { get; set; }
		public DateTime? Modified_Date { get; set; }
		public int? Modified_User { get; set; }

		public virtual ICollection<Localidad>? Localidades { get; set; }
	}

}
