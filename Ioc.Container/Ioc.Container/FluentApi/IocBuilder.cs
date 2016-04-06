using System;
using System.Collections.Generic;
using System.Linq;
using IoC.Container.Models;

namespace IoC.Container.FluentApi
{
	public class IocBuilder
	{
		internal Dictionary<Type, List<ConcreteType>> RegisteredTypes;

//		internal RegisteredType TypeContext { get; set; }
//		internal ConcreteType ConcreteTypeContext { get; set; }

		public IocBuilder(Dictionary<Type, List<ConcreteType>> registeredTypes)
		{
			RegisteredTypes = registeredTypes;
		}

		public IocConcreteBuilder Register<TContract>()
			where TContract : class
		{
			return RegisterType(typeof(TContract));
		}

		private IocConcreteBuilder RegisterType(Type type)
		{
			var typeContext = new RegisteredType {Contract = type, Concrete = new List<ConcreteType>()};

			if (RegisteredTypes.ContainsKey(type))
			{
				typeContext.Concrete = RegisteredTypes[type];
				return new IocConcreteBuilder(this, typeContext);
			}

			RegisteredTypes.Add(typeContext.Contract, typeContext.Concrete);
			
			return new IocConcreteBuilder(this, typeContext);
		}
	}
}