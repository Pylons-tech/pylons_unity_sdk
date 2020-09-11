#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrossPlatformIPC
{
    public class AndroidMessageEncoder : MessageEncoder
    {
        public static void ConformAndroidSideToIpcTarget (IPCTarget target)
        {
            j_Class.SetStatic("targetPackageName", target.androidPackageName);
            j_Class.SetStatic("targetActivityName", target.androidActivityName);
            j_Class.SetStatic("targetServiceName", target.androidServiceName);
        }

        private static AndroidJavaClass j_Class = new AndroidJavaClass("com.pylons.client.UnityAndroidBridgeActivity");
        private static AndroidJavaClass j_ClassInner = new AndroidJavaClass("com.pylons.client.UnityAndroidBridgeActivity$IntentData");
        public static AndroidMessageEncoder Prepare ()
        {
            j_ClassInner.CallStatic("prepareNewIntent");
            return new AndroidMessageEncoder();
        }

        private static Dictionary<string, T> ExtractForDataType<T> (Func<string[]> getKeys, Func<string, T> getVal)
        {
            string[] keys = getKeys();
            int len;
            if (keys != null) len = keys.Length;
            else len = 0;
            Debug.Log(len);
            Dictionary<string, T> dict = new Dictionary<string, T>(len);
            if (keys != null) foreach (string key in keys) dict.Add(key, getVal(key));
            return dict;
        }

        private static Message ReceiveFromJavaSide ()
        {
            AndroidMessageEncoder me = new AndroidMessageEncoder();
            Message m = new Message();
            m.bools = ExtractForDataType(me.GetBoolNames, me.GetBool);
            m.boolArrays = ExtractForDataType(me.GetBoolArrayNames, me.GetBoolArray);
            m.bytes = ExtractForDataType(me.GetByteNames, me.GetByte);
            m.byteArrays = ExtractForDataType(me.GetByteArrayNames, me.GetByteArray);
            m.chars = ExtractForDataType(me.GetCharNames, me.GetChar);
            m.charArrays = ExtractForDataType(me.GetCharArrayNames, me.GetCharArray);
            m.doubles = ExtractForDataType(me.GetDoubleNames, me.GetDouble);
            m.doubleArrays = ExtractForDataType(me.GetDoubleArrayNames, me.GetDoubleArray);
            m.floats = ExtractForDataType(me.GetFloatNames, me.GetFloat);
            m.floatArrays = ExtractForDataType(me.GetFloatArrayNames, me.GetFloatArray);
            m.ints = ExtractForDataType(me.GetIntNames, me.GetInt);
            m.intArrays = ExtractForDataType(me.GetIntArrayNames, me.GetIntArray);
            m.longs = ExtractForDataType(me.GetLongNames, me.GetLong);
            m.longArrays = ExtractForDataType(me.GetLongArrayNames, me.GetLongArray);
            m.shorts = ExtractForDataType(me.GetShortNames, me.GetShort);
            m.shortArrays = ExtractForDataType(me.GetShortArrayNames, me.GetShortArray);
            m.strings = ExtractForDataType(me.GetStringNames, me.GetString);
            m.stringArrays = ExtractForDataType(me.GetStringArrayNames, me.GetStringArray);
            return m;
        }
        public bool GetBool(string name) => GetBool(name, false);
        public bool GetBool(string name, bool defaultValue) => j_ClassInner.CallStatic<bool>("getBooleanExtra", new object[] { name, defaultValue });
        public bool[] GetBoolArray(string name) => j_ClassInner.CallStatic<bool[]>("getBooleanArrayExtra", new object[] { name });
        public byte GetByte(string name) => GetByte(name, 0);
        public byte GetByte(string name, byte defaultValue) => j_ClassInner.CallStatic<byte>("getByteExtra", new object[] { name, defaultValue });
        public byte[] GetByteArray(string name) => j_ClassInner.CallStatic<byte[]>("getByteArrayExtra", new object[] { name });
        public char GetChar(string name) => GetChar(name, char.MinValue);
        public char GetChar(string name, char defaultValue) => j_ClassInner.CallStatic<char>("getCharExtra", new object[] { name, defaultValue });
        public char[] GetCharArray(string name) => j_ClassInner.CallStatic<char[]>("getCharArrayExtra", new object[] { name });
        public double GetDouble(string name) => GetDouble(name, 0);
        public double GetDouble(string name, double defaultValue) => j_ClassInner.CallStatic<double>("getDoubleExtra", new object[] { name, defaultValue });
        public double[] GetDoubleArray(string name) => j_ClassInner.CallStatic<double[]>("getDoubleArrayExtra", new object[] { name });
        public float GetFloat(string name) => GetFloat(name, 0);
        public float GetFloat(string name, float defaultValue) => j_ClassInner.CallStatic<float>("getFloatExtra", new object[] { name, defaultValue });
        public float[] GetFloatArray(string name) => j_ClassInner.CallStatic<float[]>("getFloatArrayExtra", new object[] { name });
        public int GetInt(string name) => GetInt(name, 0);
        public int GetInt(string name, int defaultValue) => j_ClassInner.CallStatic<int>("getIntExtra", new object[] { name, defaultValue });
        public int[] GetIntArray(string name) => j_ClassInner.CallStatic<int[]>("getIntArrayExtra", new object[] { name });
        public long GetLong(string name) => GetLong(name, 0);
        public long GetLong(string name, long defaultValue) => j_ClassInner.CallStatic<long>("getLongExtra", new object[] { name, defaultValue });
        public long[] GetLongArray(string name) => j_ClassInner.CallStatic<long[]>("getLongArrayExtra", new object[] { name });
        public short GetShort(string name) => GetShort(name, 0);
        public short GetShort(string name, short defaultValue) => j_ClassInner.CallStatic<short>("getShortExtra", new object[] { name, defaultValue });
        public short[] GetShortArray(string name) => j_ClassInner.CallStatic<short[]>("getShortArrayExtra", new object[] { name });
        public string GetString(string name) => j_ClassInner.CallStatic<string>("getStringExtra", new object[] { name });
        public string[] GetStringArray(string name) => j_ClassInner.CallStatic<string[]>("getStringArrayExtra", new object[] { name });
        public string[] GetBoolArrayNames() => j_ClassInner.CallStatic<string[]>("getBooleanArrayExtrasKeys");
        public string[] GetBoolNames() => j_ClassInner.CallStatic<string[]>("getBooleanExtrasKeys");
        public string[] GetByteArrayNames() => j_ClassInner.CallStatic<string[]>("getByteArrayExtrasKeys");
        public string[] GetByteNames() => j_ClassInner.CallStatic<string[]>("getByteExtrasKeys");
        public string[] GetCharArrayNames() => j_ClassInner.CallStatic<string[]>("getCharArrayExtrasKeys");
        public string[] GetCharNames() => j_ClassInner.CallStatic<string[]>("getCharExtrasKeys");
        public string[] GetDoubleArrayNames() => j_ClassInner.CallStatic<string[]>("getDoubleArrayExtrasKeys");
        public string[] GetDoubleNames() => j_ClassInner.CallStatic<string[]>("getDoubleExtrasKeys");
        public string[] GetFloatArrayNames() => j_ClassInner.CallStatic<string[]>("getFloatArrayExtrasKeys");
        public string[] GetFloatNames() => j_ClassInner.CallStatic<string[]>("getFloatExtrasKeys");
        public string[] GetIntArrayNames() => j_ClassInner.CallStatic<string[]>("getIntArrayExtrasKeys");
        public string[] GetIntNames() => j_ClassInner.CallStatic<string[]>("getIntExtrasKeys");
        public string[] GetLongArrayNames() => j_ClassInner.CallStatic<string[]>("getLongArrayExtrasKeys");
        public string[] GetLongNames() => j_ClassInner.CallStatic<string[]>("getLongExtrasKeys");
        public string[] GetShortArrayNames() => j_ClassInner.CallStatic<string[]>("getShortArrayExtrasKeys");
        public string[] GetShortNames() => j_ClassInner.CallStatic<string[]>("getShortExtrasKeys");
        public string[] GetStringArrayNames() => j_ClassInner.CallStatic<string[]>("getStringArrayExtrasKeys");
        public string[] GetStringNames() => j_ClassInner.CallStatic<string[]>("getStringExtrasKeys");

        public override bool Has(string name) => j_ClassInner.CallStatic<bool>("hasExtra", new object[] { name });

        public override void Put(bool[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(bool v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Put(byte[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(byte v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Put(char[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(char v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Put(double[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(double v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Put(float[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(float v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Put(int[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(int v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Put(long[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(long v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Put(short[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(short v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Put(string[] vs, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, vs });
        }

        public override void Put(string v, string name)
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to put data in non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("putExtra", new object[] { name, v });
        }

        public override void Send()
        {
            if (direction != Direction.Outgoing) throw new InvalidOperationException("Attempted to send non-outgoing AndroidMessage");
            j_ClassInner.CallStatic("invokeServiceWithPreparedIntent");
        }

        public static bool TryReceive (out Message receivedMsg)
        {
            bool received = j_ClassInner.CallStatic<bool>("isIncomingMessageReady");
            if (received) receivedMsg = ReceiveFromJavaSide();
            else receivedMsg = null;
            return received;
        }
    }
}
#endif