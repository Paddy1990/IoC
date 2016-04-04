using System;
using IoC.Container.Core.FluentApi;

namespace IoC.Container.Core
{
	public class IocContainer : IContainer
	{
//		private readonly 

		public IocContainer(){}

		public IocContainer(Func<FluentApiContainer, FluentApiContainer> func)
		{
			var fluentApi = new FluentApiContainer();
			func.Invoke(fluentApi);
		}


	}

	public interface IContainer
	{

	}
}
