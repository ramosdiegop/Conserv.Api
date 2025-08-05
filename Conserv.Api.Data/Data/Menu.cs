using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Data
{
	[Table("menu")]
	public class Menu
	{		
		[Key]
		public int Id { get; set; }		
		public string Nombre { get; set; }
		[MaxLength(2)]
		public string Esmenu { get; set; }
		[MaxLength(2)]
		public string EsSubmenu { get; set; }
		[MaxLength(2)]
		public string Eslink { get; set; }
		[MaxLength(2)]
		public string Ejecutable { get; set; }
		public int? Idmenu { get; set; }
		public int? Idsubmenu { get; set; }
		public string? link { get; set; }
		public DateTime Created_Date { get; set; }
		public int Created_User { get; set; }
		public DateTime? Modified_Date { get; set; }
		public int? Modified_User { get; set; }


		public virtual ICollection<MenuUsuario>? MenuUsuarios { get; set; }
		

	}
}
