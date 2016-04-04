using System;
using System.Collections.Generic;
using IoC.Container.Core.Models;

namespace IoC.Container.Core.FluentApi
{
	public class FluentApiContainer
	{
		public static Dictionary<Type, List<ConcreteType>> RegisteredTypes;

		public static List<ConcreteType> RegisteredConcreteTypes;

		public static ConcreteType ConcreteType;

		public static ConcreteOptions ConcreteOptions;

		public FluentApiContainer()
		{
			
		}
//
//		public FluentApiConcreteModel RegisterType<TContract>() where TContract : class
//		{
//			ConcreteModel.RegisteredType = _iocService.AddRegisteredType<TContract>();
//			return ConcreteModel;
//		}

	}
}
