using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ioc.Container.Constants;
using Ioc.Container.Models;
using Ioc.Container.Servicecs;

namespace Ioc.Container.FluentApi
{
	public class FluentApi
	{
		internal FluentApiConcreteModel ConcreteModel;
		private readonly IIocService _iocService;

		public FluentApi()
		{
			ConcreteModel = new FluentApiConcreteModel(this);
			_iocService = new IocService();
		}

		public FluentApiConcreteModel RegisterType<TContract>() where TContract : class
		{
			ConcreteModel.Type = _iocService.AddRegisteredType<TContract>();
			return ConcreteModel;
		}

//		public FluentIocConcreteModel UseContract<TContract>() where TContract : class
//		{
//			ConcreteModel.RegisteredType = new RegisteredType
//			{
//				Contract = typeof(TContract),
//				Concrete = new List<ConcreteType>()
//			};
//
//			return ConcreteModel;
		//		}
		public FluentApi Register(Action<FluentApi> func)
		{
			func.Invoke(this);
			return this;
			//			return (TContract)ResolveType(typeof(TContract));
		}


		public TContract Resolve<TContract>() where TContract : class
		{
			return (TContract)ResolveType(typeof(TContract));
		}


		private object ResolveType(Type type)
		{
			var registeredType = GetRegisteredType(type);

			//TODO: Add ability to allow registering multiple concrete type to one interface.
			var concrete = registeredType.Concrete.First();

			var constructor = GetConstructor(concrete.Type);

			var arguments = constructor.GetParameters();

			if (concrete.LifeCycle == LifeCycle.Singleton)
			{
				if (concrete.Instance == null)
				{
					if (arguments.Length == 0)
						return CreateInstance(concrete);
				}
				else return concrete.Instance;
			}

			if (concrete.LifeCycle == LifeCycle.Transient)
			{
				//If we have no parameters then we have come to the bottom of one of the dependancy chains.
				//Here we create a new instance of the concrete type
				if (arguments.Length == 0)
					return CreateInstance(concrete);
			}

			var dependancies = ResolveDependancyChain(arguments);

			return constructor.Invoke(dependancies.ToArray());
		}

		private Object CreateInstance(ConcreteType concreteType)
		{
			concreteType.Instance = Activator.CreateInstance(concreteType.Type);
			return concreteType.Instance;
		}
		
		private List<object> ResolveDependancyChain(IEnumerable<ParameterInfo> ctorParameters)
		{
			//Loop around the constructor parameters and recursively loopthrough the dependancy chain.
			var parameters = new List<object>();
			foreach (var parameterInfo in ctorParameters)
				parameters.Add(ResolveType(parameterInfo.ParameterType));

			return parameters;
		}

		private ConstructorInfo GetConstructor(Type type)
		{
			var ctorList = type.GetConstructors();

			//look for the constructor that has the highest count of parameters
			var ctorParams = ctorList.Where(w =>
				w.GetParameters().Length == ctorList.Max(m => m.GetParameters().Length)).ToList();

			//If theres more than one, we throw an exception because there you don't know which concrete type to create an instance for.
			//TODO: Add the ability to resolve multiple concrete types to one contract. 
			//TODO: Add this as a method to the fluent api to specify what concrete type to use for each implementation.
			//TODO: E.G. RegisterFor<IFoo>().UseConcrete<foo>().UseConcrete<bar>
			//TODO: (Pass something in here to specify how to know when to use this concrete type. E.G. by name, by data attribute maybe?)
			if (ctorParams.Count() > 1)
				throw new Exception(
					string.Format("There are two many constructors to resolve the following concrete type: {0}. " +
					              "Please Specify which constructor to use.", type));

			//There should always be at least one constructor so we are safe to use .First()
			return ctorParams.First();
		}

		private RegisteredType GetRegisteredType(Type type)
		{
			var registeredType = ConcreteModel.RegisteredTypes.FirstOrDefault(x => x.Contract == type);

			if (registeredType == null)
				throw new Exception("Type not registered: " + type);

			if (!registeredType.Concrete.Any())
				throw new Exception("Type doesn't have a concrete type: " + type);
			return registeredType;
		}
	}
}