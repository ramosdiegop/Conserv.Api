using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Domain.Dtos
{
	public class MenuDto
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Esmenu { get; set; }
		public string EsSubmenu { get; set; }
		public string Eslink { get; set; }
		public string Ejecutable { get; set; }
		public int? Idmenu { get; set; }
		public int? Idsubmenu { get; set; }
		public string? link { get; set; }

	}


	public class ItemDto {

		public int Id { get; set; }
		public string label { get; set; }
        public string icon { get; set; }
        public string routerLink { get; set; }
		public List<ItemDto> items { get; set; }
		
	}


	public class MenuAccesosDto
	{
		public string key { get; set; }
		public  DataMenu data { get; set; }
		public List<MenuAccesosDto> children { get; set; }

	}


	public class DataMenu{ 
		public string name { get; set; }
	}


}
