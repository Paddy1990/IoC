using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoC.Container.FluentApi;

namespace IoC.Container.Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			var container = new IocContainer(x =>
			{
				x.RegisterType<IClass1>().ForConcrete<Class1>().AsSingleton();
				x.RegisterType<IClass2>().ForConcrete<Class2>().AsTransient();
				x.RegisterType<Class3>().ForConcrete<Class3>();
			});

			var class1 = container.Resolve<IClass1>();

			Console.WriteLine(class1.Write("Paddy", "Halle"));
			Console.ReadLine();
		}
	}
}
