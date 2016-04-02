namespace Ioc.Container.Demo
{
	public class Class1 : IClass1
	{
		public string HelloWorld(string name)
		{
			return string.Format("Hello {0}!", name);
		}
	}

	public interface IClass1
	{
		string HelloWorld(string name);
	}

	public class Class2 : IClass2
	{
		private readonly IClass1 _class1;

		public Class2(IClass1 class1)
		{
			_class1 = class1;
		}

		public string HelloWorld(string name)
		{
			return string.Format("Hello {0}!", _class1.HelloWorld(name));
		}
	}

	public interface IClass2
	{
		string HelloWorld(string name);
	}


	public class Class3 : IClass3
	{
		private readonly IClass1 _class1;
		private readonly IClass2 _class2;

		public Class3(IClass1 class1, IClass2 class2)
		{
			_class1 = class1;
			_class2 = class2;
		}

		public Class3(IClass1 class1)
		{
			_class1 = class1;
		}

		public string HelloWorld(string name)
		{
			return string.Format("Class1: {0}! Class2: {1}", _class1.HelloWorld("Paddy"), _class2.HelloWorld("Halle"));
		}
	}

	public interface IClass3
	{
		string HelloWorld(string name);
	}
}
