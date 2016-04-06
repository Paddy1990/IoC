using System;

namespace IoC.Container.Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			var container = new IocContainer(x =>
			{
				x.Register<IClass1>().Concrete<Class1>().AsSingleton();
				x.Register<IClass2>().Concrete<Class2>().Concrete<Class4>().AsTransient();
				x.Register<IClass3>().Concrete<Class3>();
			});

			var class1 = container.Resolve<IClass1>();

			Console.WriteLine(class1.Write("Paddy", "Halle"));
			Console.ReadLine();
		}
	}
}
