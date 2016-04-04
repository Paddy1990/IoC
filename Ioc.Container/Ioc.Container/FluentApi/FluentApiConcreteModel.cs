using System.Collections.Generic;
using System.Linq;
using IoC.Container.Constants;
using IoC.Container.Models;
using Ioc.Container.Constants;
using Ioc.Container.FluentApi;

namespace IoC.Container.FluentApi
{
	public class FluentApiConcreteModel
	{
		internal readonly IocBuilder IocBuilder;
		
		internal List<RegisteredType> RegisteredTypes { get; set; }
		internal RegisteredType RegisteredType { get; set; }
		internal FluentApiLifeCycleModel LifeCycleModel { get; set; } 

		public FluentApiConcreteModel(IocBuilder iocBuilder)
		{
			IocBuilder = iocBuilder;
			RegisteredTypes = new List<RegisteredType>();
			LifeCycleModel = new FluentApiLifeCycleModel(this);
		}

		public FluentApiLifeCycleModel ForConcrete<TConcrete>(string lifeCycle = LifeCycle.Transient) where TConcrete : class
		{
			LifeCycleModel.ConcreteTypeModel = new ConcreteType
			{
				Type = typeof (TConcrete),
				Options = new ConcreteOptions
				{
					LifeCycle = lifeCycle,
					Match = Match.ByName
				}
			};

			RegisteredType.Concrete.Add(LifeCycleModel.ConcreteTypeModel);

			var type = RegisteredTypes.FirstOrDefault(x => x.Contract == RegisteredType.Contract);

			if (type == null)
				RegisteredTypes.Add(RegisteredType);

			return LifeCycleModel;
		}

	}
}