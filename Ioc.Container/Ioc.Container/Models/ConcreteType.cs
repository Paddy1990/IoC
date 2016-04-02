using System;

namespace Ioc.Container.Models
{
	public class ConcreteType
	{
		public Type Type		{ get; set; }
		public string LifeCycle { get; set; }
		public Object Instance { get; set; }
	}
}