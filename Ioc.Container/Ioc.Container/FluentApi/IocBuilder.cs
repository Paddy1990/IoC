using System;
using System.Collections.Generic;
using System.Linq;
using IoC.Container.Models;

namespace IoC.Container.FluentApi
{
	public class IocBuilder
	{
		internal Dictionary<Type, List<ConcreteType>> RegisteredTypes;

		internal RegisteredType TypeContext;
		internal ConcreteType ConcreteTypeContext;

		public IocBuilder(Dictionary<Type, List<ConcreteType>> registeredTypes)
		{
			RegisteredTypes = registeredTypes;
		}

		public IocConcreteBuilder RegisterType<TContract>()
			where TContract : class
		{
			return Register(typeof(TContract));
		}

		private IocConcreteBuilder Register(Type type)
		{
			//Should I throw an error here or just return the container?
			if (RegisteredTypes.Any(x => x.Key == type))
				throw new Exception(string.Format("The Type '{0}' has already been registered!", type.FullName));
//				return this;

			ConcreteTypeContext = new ConcreteType();

			TypeContext = new RegisteredType
			{
				Contract = type,
				Concrete = new List<ConcreteType>()
			};

			//Not sure if I should add the contract to the Dictionary here or wait until the UseConcrete method is called?
			//			_registeredTypes.Add(Type.Contract, Type.Concrete);

			return new IocConcreteBuilder(this);
		}
	}
}