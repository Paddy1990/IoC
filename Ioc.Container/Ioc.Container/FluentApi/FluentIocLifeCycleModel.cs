using Ioc.Container.Constants;
using Ioc.Container.Models;

namespace Ioc.Container.FluentApi
{
	public class FluentIocLifeCycleModel
	{
		internal readonly FluentIocConcreteModel FluentIocLifeCycle;

		internal ConcreteType ConcreteTypeModel { get; set; }

		public FluentIocLifeCycleModel(FluentIocConcreteModel fluentIocLifeCycle)
		{
			FluentIocLifeCycle = fluentIocLifeCycle;
		}

		public FluentIocLifeCycleModel AsSingleton()
		{
			ConcreteTypeModel.LifeCycle = LifeCycle.Singleton;
			return this;
		}

		public FluentIocLifeCycleModel AsTransient()
		{
			ConcreteTypeModel.LifeCycle = LifeCycle.Transient;
			return this;
		}

		public FluentIocLifeCycleModel PerRequest()
		{
			ConcreteTypeModel.LifeCycle = LifeCycle.PerRequest;
			return this;
		}
	}
}