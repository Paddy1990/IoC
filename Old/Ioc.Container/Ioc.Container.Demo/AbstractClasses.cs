using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ioc.Container.Demo
{
	public abstract class AbstractClass1
	{
		public Dictionary<string, string> People { get; set; }
		private int _counter { get; set; }

		public int Counter { get { return _counter; } set { _counter = value; } }

		public AbstractClass1()
		{
			People = new Dictionary<string, string>();
		}

		public string AddPerson(string name)
		{
			var person = "Hello my name is " + name;
			Counter++;
			People[name] = person;
			return People[name];
		}

		public virtual List<string> GetPeople()
		{
			return People.Values.ToList();
		}

		public abstract string GetPerson(string name);

		public abstract string RemovePerson(string name);
	}

	public class DerivedClass1 : AbstractClass1
	{
		public override List<string> GetPeople()
		{
			var basePeople = base.GetPeople();
			return basePeople.OrderByDescending(s => s).ToList();
		}

		public override string GetPerson(string name)
		{
			return People.ContainsKey(name) ? People[name] : "Could not find person " + name;
		}

		public override string RemovePerson(string name)
		{
			var personRemoved = People.ContainsKey(name) && People.Remove(name);
			
			if(personRemoved)
				Counter-- ;

			return personRemoved 
				? name + " was removed succesfully!"
				: "Could not find person " + name;
		}
	}
}
