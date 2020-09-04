using System;
using System.Reflection;

namespace CrossPlatformIPC
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultIPCTarget : Attribute
    {
        public DefaultIPCTarget ()
        {

        }

        public static IPCTarget Get ()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var types = assembly.GetTypes();
                foreach (var type in types) if (type.IsDefined(typeof(DefaultIPCTarget)))
                        return type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as IPCTarget;
            }
            return null;
        }
    }
}