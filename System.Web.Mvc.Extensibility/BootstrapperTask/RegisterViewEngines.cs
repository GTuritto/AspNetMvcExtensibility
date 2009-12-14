#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility
{
    using Microsoft.Practices.ServiceLocation;

    public class RegisterViewEngines : BootstrapperTaskBase
    {
        public RegisterViewEngines(ViewEngineCollection viewEngines)
        {
            Invariant.IsNotNull(viewEngines, "viewEngines");

            ViewEngines = viewEngines;
        }

        protected ViewEngineCollection ViewEngines
        {
            get;
            private set;
        }

        protected override void ExecuteCore(IServiceLocator serviceLocator)
        {
            serviceLocator.GetAllInstances<IViewEngine>()
                          .Each(engine =>
                                  {
                                      if (!ViewEngines.Contains(engine))
                                      {
                                          ViewEngines.Add(engine);
                                      }
                                  });
        }
    }
}