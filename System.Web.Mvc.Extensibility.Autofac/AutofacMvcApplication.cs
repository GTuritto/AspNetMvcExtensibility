#region Copyright
/// Copyright (c) 2009, Kazi Manzur Rashid <kazimanzurrashid@gmail.com>.
/// This source is subject to the Microsoft Public License. 
/// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
/// All other rights reserved.
#endregion

namespace System.Web.Mvc.Extensibility.Autofac
{
    public class AutofacMvcApplication : ExtendedMvcApplicationBase
    {
        protected override IBootstrapper CreateBootstrapper()
        {
            return new AutofacBootstrapper(BuildManagerWrapper.Current);
        }
    }
}