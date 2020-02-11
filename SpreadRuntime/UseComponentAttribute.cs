using System;

namespace SpreadRuntime
{
    public class UseComponentAttribute : Attribute
    {
        public Type ComponentType { get; }

        public UseComponentAttribute(Type T)
        {
            ComponentType = T;
        }
    }
}
