using Conserv.Api.Data.Data;
using Conserv.Api.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Interfaz
{
	public interface IMapping
	{
		List<ItemDto> MappMenu(List<MenuDto> menu);
		List<MenuAccesosDto> MappMenuCrud(List<Menu> menuAccesos);
	}
}
