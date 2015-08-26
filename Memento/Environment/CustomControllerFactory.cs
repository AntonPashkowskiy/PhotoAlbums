using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Memento.App_Start;

namespace Memento.Environment
{
	public class CustomControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				return null;
			}

			return (IController)NinjectConfig.StandartKernel.Get(controllerType);
		}
	}
}