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
		private ConcreteType _concreteType;

		public IocConcreteBuilder(IocBuilder iocBuilder)
		{
			_iocBuilder = iocBuilder;

			_registeredTypes = iocBuilder.RegisteredTypes;
			_typeContext = iocBuilder.TypeContext;
			_concreteType = iocBuilder.ConcreteTypeContext;
		}

		public IocLifeCycleBuilder ForConcrete<TConcrete>(string lifeCycle = LifeCycle.Transient) where TConcrete : class
		{
			_concreteType.Type = typeof (TConcrete);
			_concreteType.Options = new ConcreteOptions
			{
				LifeCycle = lifeCycle
			};

			_typeContext.Concrete.Add(_concreteType);

			if (_registeredTypes.Any(x => x.Key == _typeContext.Contract))
				_registeredTypes[_typeContext.Contract].Add(_concreteType);
			else
				_registeredTypes.Add(_typeContext.Contract, _typeContext.Concrete);

			return new IocLifeCycleBuilder(_iocBuilder);
		}
	}
}