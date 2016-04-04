namespace Ioc.Container.IocContainerOld
{
	public interface IIocContainerOld
	{
		void Register<TContract, TConcrete>();
		void Register<TContract, TConcrete>(string lifeCycle);
//		void AutoRegister<TContract, TConcrete>(string lifeCycle);
	}
}