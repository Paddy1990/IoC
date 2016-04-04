using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using IoC.Container.Models;
using Ioc.Container.Constants;

namespace IoC.Container.FluentApi
{
	public class IocBuilder2
	{
		private List<RegisteredType> _registeredTypes;

		public RegisteredType Type;

		public IocBuilder2(List<RegisteredType> registeredTypes)
		{
			_registeredTypes = registeredTypes;
		}

		public IocBuilder2 RegisterType<TContract>()
			where TContract : class
		{
			return Register(typeof(TContract));
		}

		private ConcreteType _concreteType;

		public IocBuilder2 ForConcrete<TConcrete>(string lifeCycle = LifeCycle.Transient) where TConcrete : class
		{
			var type = _registeredTypes.Any(x => x.Contract == Type.Contract);

			_concreteType = new ConcreteType
			{
				Type = typeof (TConcrete),
				Options = new ConcreteOptions
				{
					LifeCycle = lifeCycle
				}
			};

			Type.Concrete.Add(_concreteType);


			if (type == false)
				_registeredTypes.Add(Type);

			return this;
		}

		private IocBuilder2 Register(Type type)
		{
			//Should I throw an error here or just return the container?
			if (_registeredTypes.Any(x => x.Contract == type))
				throw new Exception(string.Format("The Type '{0}' has already been registered!", type.FullName));
//				return this;

			Type = new RegisteredType
			{
				Contract = type,
				Concrete = new List<ConcreteType>()
			};
			
			//Not sure if I should add the contract to the Dictionary here or wait until the UseConcrete method is called?
//			_registeredTypes.Add(Type.Contract, Type.Concrete);

			return this;
		}

	}
}
