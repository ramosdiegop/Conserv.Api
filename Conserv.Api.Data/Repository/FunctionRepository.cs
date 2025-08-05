using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conserv.Api.Data.Repository
{
	public class FunctionRepository
	{
		public FunctionRepository()
		{
		}

		public List<string[]> VerifyArrayFilter(string[] Avalue, string[] Afield)
		{

			var list = new List<string[]>();

			var auxarray = Avalue.Where(n => n == null).Select((source, Index) => Index).OrderByDescending(o => o).ToArray();
			var destval = new List<string>(Avalue);
			var destfie = new List<string>(Afield);

			foreach (var item in auxarray)
			{
				destval.RemoveAt(item);
				destfie.RemoveAt(item);

			}

			list.Add(destval.ToArray());
			list.Add(destfie.ToArray());


			return list;
		}

	}
}
