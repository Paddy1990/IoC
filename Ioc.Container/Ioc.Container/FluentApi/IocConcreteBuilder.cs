using System;
using System.Collections.Generic;
using System.Linq;
using IoC.Container.Models;
using Ioc.Container.Constants;

namespace IoC.Container.FluentApi
{
	public class IocConcreteBuilder
	{
		private readonly IocBuilder _iocBuilder;

		private readonly Dictionary<Type, List<ConcreteType>> _registeredTypes;
		private readonly RegisteredType _typeContext;

		public IocConcreteBuilder(IocBuilder iocBuilder, RegisteredType typeContext)
		{
			_iocBuilder = iocBuilder;

			_registeredTypes = iocBuilder.RegisteredTypes;
			_typeContext = typeContext;
		}

//		public IocLifeCycleBuilder ForConcrete<TConcrete>(string lifeCycle = LifeCycle.Transient) where TConcrete : class
		public IocConcreteBuilder Concrete<TConcrete>() where TConcrete : class
		{
			var concreteType = new ConcreteType
			{
				Type = typeof(TConcrete),
				LifeCycle = LifeCycle.Transient
			};

			if (_registeredTypes.ContainsKey(_typeContext.Contract))
				return AddConcreteType(concreteType);
			
			_registeredTypes.Add(_typeContext.Contract, _typeContext.Concrete);
			return new IocConcreteBuilder(_iocBuilder, _typeContext);
		}

		public void AsSingleton()
		{
			_typeContext.Concrete.ForEach(x => x.LifeCycle = LifeCycle.Singleton);
		}

		public void AsTransient()
		{
			_typeContext.Concrete.ForEach(x => x.LifeCycle = LifeCycle.Transient);
		}

		public void PerRequest()
		{
			_typeContext.Concrete.ForEach(x => x.LifeCycle = LifeCycle.PerRequest);
		}

		private IocConcreteBuilder AddConcreteType(ConcreteType concreteType)
		{
			var registeredType = _registeredTypes[_typeContext.Contract];

			if (registeredType.Any(x => x.Type == concreteType.Type))
				throw new Exception(string.Format("The concrete type has already been registered: {0}", concreteType.Type));

			registeredType.Add(concreteType);
			return new IocConcreteBuilder(_iocBuilder, _typeContext);
		}

	}
}