namespace Ioc.Container.IocContainer
{
	public interface IIocContainer
	{
		void Register<TContract, TConcrete>();
		void Register<TContract, TConcrete>(string lifeCycle);
//		void AutoRegister<TContract, TConcrete>(string lifeCycle);
	}
}