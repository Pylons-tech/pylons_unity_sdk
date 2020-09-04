using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedException
{
    public readonly string JvmException;
    public readonly string Message;
    public readonly string StackTrace;

    public PassedException(string jvmException, string message, string stackTrace)
    {
        JvmException = jvmException;
        Message = message;
        StackTrace = stackTrace;
    }
}
