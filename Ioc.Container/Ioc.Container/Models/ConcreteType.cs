using System;

namespace IoC.Container.Models
{
	public class ConcreteType
	{
		public Type Type { get; set; }
		public Object Instance { get; set; }
		public ConcreteOptions Options { get; set; }
	}

	public class ConcreteOptions
	{
		public string Match { get; set; }
		public string LifeCycle { get; set; }
	}
}
