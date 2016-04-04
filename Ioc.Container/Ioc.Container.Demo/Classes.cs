namespace IoC.Container.Demo
{
	public class Class1 : IClass1
	{
		private readonly IClass2 _class2;
		private readonly IClass3 _class3;

		public Class1(IClass2 class2, IClass3 class3)
		{
			_class2 = class2;
			_class3 = class3;
		}

		public string Write(string firstName, string lastName)
		{
			return string.Format("First Name: {0} - Last Name: {1}", _class2.Write(firstName), _class3.Write(lastName));
		}
	}

	public class Class2 : IClass2
	{
		private readonly IClass3 _class3;

		public Class2(IClass3 class3)
		{
			_class3 = class3;
		}

		public string Write(string text)
		{
			return _class3.Write(text);
		}
	}

	public class Class3 : IClass3
	{
		public string Write(string text)
		{
			return string.Format("Hello {0}", text);
		}
	}

	public interface IClass1
	{
		string Write(string firstName, string lastName);
	}

	public interface IClass2
	{
		string Write(string text);
	}

	public interface IClass3
	{
		string Write(string text);
	}
}
