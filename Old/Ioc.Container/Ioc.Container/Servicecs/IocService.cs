using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ioc.Container.FluentApi;
using Ioc.Container.Models;

namespace Ioc.Container.Servicecs
{
	public class IocService : IIocService
	{
		internal FluentApiConcreteModel ConcreteExtension;

		public RegisteredType AddRegisteredType<TContract>() where TContract : class
		{
			return new RegisteredType
			{
				Contract = typeof(TContract),
				Concrete = new List<ConcreteType>()
			};
		}

		public ConcreteType AddConcreteType<TConcrete>(string lifeCycle) where TConcrete : class
		{
			return new ConcreteType
			{
				Type = typeof (TConcrete),
				LifeCycle = lifeCycle
			};
		}

	}

	public interface IIocService
	{
		RegisteredType AddRegisteredType<TContract>() where TContract : class;
	}
}
