namespace System.Web.Mvc.Extensibility
{
    using Collections.Generic;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true), CLSCompliant(false)]
    public sealed class BindingTypesAttribute : Attribute
    {
        public BindingTypesAttribute(params Type[] types)
        {
            Invariant.IsNotNull(types, "types");

            Types = types;
        }

        public IEnumerable<Type> Types
        {
            get;
            private set;
        }
    }
}