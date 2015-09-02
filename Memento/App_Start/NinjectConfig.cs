using System.Web.Configuration;
using EFDataProvider.Realization;
using Entities.Interfaces;
using Ninject;
using Ninject.Web.Common;
using ServiceLayer;

// Method start must execute only one time ApplicationStart method before.
// If you try excecute this method more, you will get exception.
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Memento.App_Start.NinjectConfig), "Start")]

namespace Memento.App_Start
{
	public static class NinjectConfig
	{
		private static IKernel _kernel;

		public static IKernel StandartKernel 
		{ 
			get 
			{
				return _kernel;
			} 
		}

		public static bool StartCalled { get; set; }

		public static void Start()
		{
			_kernel = new StandardKernel();

			_kernel.Bind<IUnitOfWork>()
				   .To<EFUnitOfWork>()
				   .InRequestScope()
				   .WithConstructorArgument(
						"connectionString",
						WebConfigurationManager.ConnectionStrings["PhotoAlbumsDBWork"].ConnectionString
					);

			_kernel.Bind<IDataService>()
				   .To<DataService>()
				   .InRequestScope();

			StartCalled = true;
		}
	}
}