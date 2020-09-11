using System;
using System.Reflection;

namespace CrossPlatformIpc
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultIpcTarget : Attribute
    {
        public DefaultIpcTarget ()
        {

        }

        public static IpcTarget Get ()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var types = assembly.GetTypes();
                foreach (var type in types) if (type.IsDefined(typeof(DefaultIpcTarget)))
                        return type.GetConstructor(new Type[] { }).Invoke(new object[] { }) as IpcTarget;
            }
            return null;
        }
    }
}