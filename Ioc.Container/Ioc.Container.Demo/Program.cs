using System;
using Ioc.Container.FluentApi;

namespace Ioc.Container.Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			var c = new FluentIoc();

			c.RegisterType<IClass1>().ForConcrete<Class1>().AsSingleton();
			c.RegisterType<IClass2>().ForConcrete<Class2>().AsTransient();
			c.RegisterType<IClass3>().ForConcrete<Class3>();
			
			
			var class3 = c.Resolve<IClass3>();

			Console.WriteLine(class3.HelloWorld("Paddy"));
			Console.ReadLine();
		}
	}
}
