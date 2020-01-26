using System;
using System.Collections.Generic;
using System.Reflection;

namespace SpreadRuntime.Bootstrap
{
    public static class AssemblyLoader
    {
        static readonly string APPLICATION_CLASSNAME = "Application";
        static readonly Type APPLICATION_INTERFACE = typeof(ISpreadApplication);

        public static Type GetApplicationType(string assemblyPath)
        {
            var assembly = Assembly.LoadFile(assemblyPath);
            var name = assembly.GetName();
            var type = assembly.GetType(name.Name + "." + APPLICATION_CLASSNAME);

            return type;
        }

        public static bool IsAssemblyValid(string assemblyPath)
        {
            Type appType = GetApplicationType(assemblyPath);
            if (appType == null) return false;

            var interfaces = new List<Type>(appType.GetInterfaces());
            return interfaces.Contains(APPLICATION_INTERFACE);
        }

        public static ISpreadApplication LoadAssembly(string assemblyPath)
        {
            Type appType = GetApplicationType(assemblyPath);
            ISpreadApplication app = appType.GetConstructor(new Type[] { }).Invoke(new object[] { }) as ISpreadApplication;
            return app;
        }
    }
}
