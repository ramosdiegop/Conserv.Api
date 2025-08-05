using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Authentication
{
	public class Authenticate  : Attribute, IAuthorizationFilter
	{

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var key = context.HttpContext.Request.Headers["Authentication-key"].ToString();
			var apikey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("Authenticationkey");
			if (key != null)
			{
				if (key == apikey)
				{
					return;
				}
			}

			context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
			return;
		}
	}
}
