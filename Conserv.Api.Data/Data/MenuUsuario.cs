using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Data
{
	[Table("menu_usuario")]
	public class MenuUsuario
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Usuarios")]
		public int Usuario_id { get; set; }
		[ForeignKey("Menues")]
		public int Menu_id { get; set; }
		public DateTime Created_Date { get; set; }
		public int Created_User { get; set; }
		public DateTime? Modified_Date { get; set; }
		public int? Modified_User { get; set; }


		public virtual Usuario? Usuarios { get; set; }
		public virtual Menu? Menues { get; set; }

	}
}
