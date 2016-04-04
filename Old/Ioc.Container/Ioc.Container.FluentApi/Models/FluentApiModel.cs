using System;
using System.Collections.Generic;

namespace Ioc.Container.FluentApi.Models
{
	public class FluentApiModel
	{
		public static Dictionary<Type, List<ConcreteType>> RegisteredTypes;

		public static List<ConcreteType> RegisteredConcreteTypes;

		public static ConcreteType ConcreteType;

		public static ConcreteOptions ConcreteOptions;


//		public FluentApiConcreteModel RegisterType<TContract>() where TContract : class
//		{
//			ConcreteModel.RegisteredType = _iocService.AddRegisteredType<TContract>();
//			return ConcreteModel;
//		}

	}
}
