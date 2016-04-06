using System;
using Ioc.Container.FluentApi;

namespace Ioc.Container.Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			var c = new FluentApi.FluentApi();

//			c.RegisterType<IClass1>().ForConcrete<Class1>().AsSingleton();
//			c.RegisterType<IClass2>().ForConcrete<Class2>().AsTransient();
//			c.RegisterType<IClass3>().ForConcrete<Class3>();

			c.Register(_ =>
			{
				_.RegisterType<IClass1>().ForConcrete<Class1>()..AsSingleton();
				_.RegisterType<IClass2>().ForConcrete<Class2>().AsTransient();
				_.RegisterType<IClass3>().ForConcrete<Class3>();
			});
			
			var class3 = c.Resolve<IClass3>();

			Console.WriteLine(class3.HelloWorld("Paddy"));

			var derivedClass = new DerivedClass1();

			Console.WriteLine(derivedClass.AddPerson("Paddy"));
			Console.WriteLine(derivedClass.AddPerson("Dave"));
			Console.WriteLine( derivedClass.AddPerson("Addy"));
			Console.WriteLine(derivedClass.AddPerson("Wilson"));

			Console.WriteLine(string.Join(", ", derivedClass.GetPeople()));

			Console.WriteLine(derivedClass.GetPerson("Paddy"));
			Console.WriteLine(derivedClass.GetPerson("adfsdfg"));

			Console.WriteLine(derivedClass.RemovePerson("asdhfh"));
			Console.WriteLine(derivedClass.RemovePerson("Addy"));

			Console.ReadLine();
		}
	}
}
