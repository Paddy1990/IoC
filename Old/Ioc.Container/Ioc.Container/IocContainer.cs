using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ioc.Container.FluentApi.Models;

namespace Ioc.Container
{
	public class IocContainer : IContainer
	{
		public IocContainer(Func<FluentApiModel, FluentApiModel> func)
		{
			var fluentApi = new FluentApiModel();
			func.Invoke(fluentApi);

		}
	}

	public interface IContainer
	{

	}
}
