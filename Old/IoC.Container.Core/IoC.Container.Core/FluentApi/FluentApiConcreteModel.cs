using System.Collections.Generic;
using System.Linq;
using IoC.Container.Core.Models;
using Ioc.Container.Core.Constants;
using Ioc.Container.FluentApi;

namespace IoC.Container.Core.FluentApi
{
	public class FluentApiConcreteModel
	{
		internal readonly FluentApiContainer FluentApi;
		
		internal List<RegisteredType> RegisteredTypes { get; set; }
		internal RegisteredType RegisteredType { get; set; }
		internal FluentApiLifeCycleModel LifeCycleModel { get; set; } 

		public FluentApiConcreteModel(FluentApi fluentApi)
		{
			FluentApi = fluentApi;
			RegisteredTypes = new List<RegisteredType>();
			LifeCycleModel = new FluentApiLifeCycleModel(this);
		}

		public FluentApiLifeCycleModel ForConcrete<TConcrete>(string lifeCycle = LifeCycle.Transient) where TConcrete : class
		{
			LifeCycleModel.ConcreteTypeModel = new ConcreteType
			{
				Type = typeof (TConcrete),
				LifeCycle = lifeCycle
			};

			RegisteredType.Concrete.Add(LifeCycleModel.ConcreteTypeModel);

			var type = RegisteredTypes.FirstOrDefault(x => x.Contract == RegisteredType.Contract);

			if (type == null)
				RegisteredTypes.Add(RegisteredType);

			return LifeCycleModel;
		}

	}
}