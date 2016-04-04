using IoC.Container.FluentApi;
using IoC.Container.Models;
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
			ConcreteTypeModel.Options.LifeCycle = LifeCycle.Singleton;
			return this;
		}

		public FluentApiLifeCycleModel AsTransient()
		{
			ConcreteTypeModel.Options.LifeCycle = LifeCycle.Transient;
			return this;
		}

		public FluentApiLifeCycleModel PerRequest()
		{
			ConcreteTypeModel.Options.LifeCycle = LifeCycle.PerRequest;
			return this;
		}
	}
}