using System;
using System.Collections.Generic;
using Ioc.Container.FluentApi.Models;

namespace Ioc.Container
{
	public class FluentApiModel
	{
		public static Dictionary<Type, List<ConcreteType>> RegisteredTypes;

		public static List<ConcreteType> RegisteredConcreteTypes;

		public static ConcreteType ConcreteType;

		public static ConcreteOptions ConcreteOptions;
	}
}
