using System;
using System.Collections.Generic;

namespace Ioc.Container.Models
{
	public class RegisteredType
	{
		public Type Contract { get; set; }
		public List<ConcreteType> Concrete { get; set; }
	}
}