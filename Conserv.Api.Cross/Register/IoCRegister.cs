using Conserv.Api.Data.Interfaz;
using Conserv.Api.Data.Repository;
using Conserv.Api.Data.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Cross.Register
{
	public static class IoCRegister
	{

		public static IServiceCollection AddRegistration(this IServiceCollection services)
		{
			AddRegisterRepositories(services);
			AddRegisterServices(services);

			return services;
		}

		private static IServiceCollection AddRegisterRepositories(this IServiceCollection services)
		{
			//Services Interfaz Repository
			services.AddTransient<IProvincia, ProvinciaRepository>();
			services.AddTransient<ILocalidad, LocalidadRepository>();
			services.AddTransient<IMapping, MappingProfile>();
			services.AddTransient<IGenero, GeneroRepository>();
			services.AddTransient<ITipoDocumento, TipoDocumentoRepository>();
			services.AddTransient<IUsuario, UsuarioRepository>();
			services.AddTransient<IMenu, MenuRepository>();


			return services;
		}

		private static IServiceCollection AddRegisterServices(this IServiceCollection services)
		{

			return services;
		}

	}
}
