using IoC.Container.Core.FluentApi;
using Ioc.Container.Constants;

namespace Ioc.Container.FluentApi
{
	public class FluentApiLifeCycleModel
	{
		internal readonly FluentApiConcreteModel FluentApiLifeCycle;

		internal ConcreteType ConcreteTypeModel { get; set; }

		public FluentApiLifeCycleModel(FluentApiConcreteModel fluentApiLifeCycle)
		{
			FluentApiLifeCycle = fluentApiLifeCycle;
		}

		public FluentApiLifeCycleModel AsSingleton()
		{
			ConcreteTypeModel.LifeCycle = LifeCycle.Singleton;
			return this;
		}

		public FluentApiLifeCycleModel AsTransient()
		{
			ConcreteTypeModel.LifeCycle = LifeCycle.Transient;
			return this;
		}

		public FluentApiLifeCycleModel PerRequest()
		{
			ConcreteTypeModel.LifeCycle = LifeCycle.PerRequest;
			return this;
		}
	}
}