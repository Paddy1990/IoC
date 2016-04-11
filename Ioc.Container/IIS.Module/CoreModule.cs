using System.Web;

namespace IIS.Module
{
	public class CoreModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
		}

		public void Dispose()
		{
			throw new System.NotImplementedException();
		}
	}
}
