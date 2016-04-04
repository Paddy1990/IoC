using System;
using System.Collections.Generic;

namespace IoC.Container.Core.Models
{
	public class IocContainerModel
	{
		public static Dictionary<Type, List<ConcreteType>> RegisteredTypes;
	}
}
