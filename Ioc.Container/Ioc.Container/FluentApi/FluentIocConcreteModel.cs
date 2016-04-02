using System.Collections.Generic;
using System.Linq;
using Ioc.Container.Constants;
using Ioc.Container.Models;

namespace Ioc.Container.FluentApi
{
	public class FluentIocConcreteModel
	{
		internal readonly FluentIoc FluentIoc;
		
		internal List<RegisteredType> RegisteredTypes { get; set; }
		internal RegisteredType RegisteredType { get; set; }
		internal FluentIocLifeCycleModel LifeCycleModel { get; set; } 

		public FluentIocConcreteModel(FluentIoc fluentIoc)
		{
			FluentIoc = fluentIoc;
			RegisteredTypes = new List<RegisteredType>();
			LifeCycleModel = new FluentIocLifeCycleModel(this);
		}

		public FluentIocLifeCycleModel ForConcrete<TConcrete>(string lifeCycle = LifeCycle.Transient) where TConcrete : class
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