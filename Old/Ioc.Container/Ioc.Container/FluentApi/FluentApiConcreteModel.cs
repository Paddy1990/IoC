using System.Collections.Generic;
using System.Linq;
using Ioc.Container.Constants;
using Ioc.Container.Models;

namespace Ioc.Container.FluentApi
{
	public class FluentApiConcreteModel
	{
		internal readonly FluentApi FluentApi;
		
		internal List<RegisteredType> RegisteredTypes { get; set; }
		internal RegisteredType Type { get; set; }

		private ConcreteType ConcreteType { get; set; }

		public FluentApiConcreteModel(FluentApi fluentApi)
		{
			FluentApi = fluentApi;
			RegisteredTypes = new List<RegisteredType>();
		}

		public FluentApiConcreteModel ForConcrete<TConcrete>(string lifeCycle = LifeCycle.Transient) where TConcrete : class
		{
			ConcreteType = new ConcreteType
			{
				Type = typeof (TConcrete),
				LifeCycle = lifeCycle
			};

			Type.Concrete.Add(ConcreteType);

			var type = RegisteredTypes.FirstOrDefault(x => x.Contract == this.Type.Contract);

			if (type == null)
				RegisteredTypes.Add(this.Type);

			return this;
		}

		public FluentApiConcreteModel AsSingleton()
		{
			ConcreteType.LifeCycle = LifeCycle.Singleton;
			return this;
		}

		public FluentApiConcreteModel AsTransient()
		{
			ConcreteType.LifeCycle = LifeCycle.Transient;
			return this;
		}

		public FluentApiConcreteModel PerRequest()
		{
			ConcreteType.LifeCycle = LifeCycle.PerRequest;
			return this;
		}

	}
}