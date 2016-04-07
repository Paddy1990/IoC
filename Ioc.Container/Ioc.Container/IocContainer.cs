using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IoC.Container.FluentApi;
using IoC.Container.Models;
using Ioc.Container.Constants;

namespace IoC.Container
{
	public class IocContainer : IContainer
	{
		private Dictionary<Type, List<ConcreteType>> RegisteredTypes { get; set; }

		private IocContainer()
		{
			RegisteredTypes = new Dictionary<Type, List<ConcreteType>>();
		}

		public IocContainer(Action<IocBuilder> func) : this()
		{
			var fluentContainer = new IocBuilder(RegisteredTypes);

			if (func != null)
				func.Invoke(fluentContainer);
		}

		public TContract Register<TContract, TConcrete>() where TContract : class
		{
			throw new NotImplementedException();
		}

		public TContract Resolve<TContract>() where TContract : class
		{
			return (TContract)ResolveType(typeof(TContract));
		}
		
		private object ResolveType(Type type)
		{
			if (type.IsGenericType && 
				type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				var concreteTypes = GetConcreteTypes(type.GetGenericArguments().First());
				return ResolveList(concreteTypes, type);
			}

			var registeredType = GetConcreteTypes(type);

			var concrete = registeredType.First();
			return InvokeConstructor(concrete);
		}

		private IEnumerable ResolveList(IEnumerable<ConcreteType> concreteTypes, Type type)
		{
			var a = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type.GetGenericArguments()));

			foreach (var implementation in concreteTypes)
				a.Add(InvokeConstructor(implementation));

			return a;
		}

		private object InvokeConstructor(ConcreteType concreteType)
		{
			var constructor = GetConstructor(concreteType.Type);

			var arguments = constructor.GetParameters();

			if (concreteType.LifeCycle == LifeCycle.Singleton)
			{
				if (concreteType.Instance == null)
				{
					if (arguments.Length == 0)
						return CreateInstance(concreteType);
				}
				else return concreteType.Instance;
			}

			if (concreteType.LifeCycle == LifeCycle.Transient)
			{
				//If we have no parameters then we have come to the bottom of one of the dependancy chain.
				//Here we create a new instance of the concrete type
				if (arguments.Length == 0)
					return CreateInstance(concreteType);
			}

//			var dependancies = ResolveDependancyChain<TContract>(arguments);
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
			//Loop around the constructor parameters and recursively loop through the dependancy chain.
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

		private List<ConcreteType> GetConcreteTypes(Type type)
		{
			var registeredType = RegisteredTypes.FirstOrDefault(x => x.Key == type).Value;

			if (registeredType == null)
				throw new Exception("Type not registered: " + type);

			if (!registeredType.Any())
				throw new Exception("Type doesn't have a concrete type: " + type);

			return registeredType;
		}
	}

	public interface IContainer
	{
		TContract Register<TContract, TConcrete>() where TContract : class;

		TContract Resolve<TContract>() where TContract : class;
	}
}
