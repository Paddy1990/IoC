using System;
using System.Collections.Generic;
using Ioc.Container.Constants;
using Ioc.Container.Models;

namespace Ioc.Container.IocContainer
{
	public class IocContainer : IIocContainer
	{
		private IDictionary<Type, ConcreteType> _registeredTypes;

		public IocContainer()
		{
			_registeredTypes = new Dictionary<Type, ConcreteType>();
		}

		public void Register<TContract, TConcrete>()
		{
			Register<TContract, TConcrete>(LifeCycle.Transient);
		}

		public void Register<TContract, TConcrete>(string lifeCycle)
		{
			_registeredTypes[typeof(TContract)] = 
				new ConcreteType { LifeCycle = lifeCycle, Type = typeof(TConcrete) };
		}
	}
}
