#define PYLONS_SDK_ITEMSCHEMA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using PylonsSdk.Tx;

namespace PylonsSdk.ItemSchema
{
    public abstract class ItemSchema
    {
        [Flags]
        public enum ConstraintDescriptorFlags
        {
            NONE = 0,
            MUST_MATCH_EXACTLY = 1,
            MUST_NOT_MATCH = 1 << 1,
            MUST_BE_MORE_THAN = 1 << 2,
            MUST_BE_MORE_THAN_OR_EQUAL_TO = 1 << 3,
            MUST_BE_LESS_THAN = 1 << 4,
            MUST_BE_LESS_THAN_OR_EQUAL_TO = 1 << 5,
            MUST_DIVIDE_BY = 1 << 6,
            MUST_NOT_DIVIDE_BY = 1 << 7,
            MUST_CONTAIN_STRING = 1 << 8,
            MUST_NOT_CONTAIN_STRING = 1 << 9,
            MUST_BEGIN_WITH_STRING = 1 << 10,
            MUST_NOT_BEGIN_WITH_STRING = 1 << 11,
            MUST_END_WITH_STRING = 1 << 12,
            MUST_NOT_END_WITH_STRING = 1 << 13
        }

        [Flags]
        public enum FieldDescriptorFlags
        {
            NONE = 0,
            DEFAULT_VALUE_IF_NOT_FOUND = 1
        }

        [Flags]
        public enum DateTimeConstraintDescriptorFlags
        {
            NONE = 0,
            BEFORE_DATE = 1,
            AFTER_DATE = 1 << 1,
            BEFORE_NOW = 1 << 3,
            AFTER_NOW = 1 << 4
        }

        private sealed class DateTimeConstraintDescriptor
        {
            private DateTimeConstraintDescriptorFlags[] flagSets;
            private DateTime[] values;

            public DateTimeConstraintDescriptor(DateTimeConstraintDescriptorFlags[] flagSets, DateTime[] values)
            {
                this.flagSets = flagSets;
                this.values = values;
            }

            public bool Check(DateTime d)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    var flags = flagSets[i];
                    var value = values[i];
                    if (flags.HasFlag(DateTimeConstraintDescriptorFlags.AFTER_DATE) && d <= value) return false;
                    else if (flags.HasFlag(DateTimeConstraintDescriptorFlags.BEFORE_DATE) && d >= value) return false;
                    else if (flags.HasFlag(DateTimeConstraintDescriptorFlags.AFTER_NOW) && d <= DateTime.Now.ToUniversalTime()) return false;
                    else if (flags.HasFlag(DateTimeConstraintDescriptorFlags.BEFORE_NOW) && d >= DateTime.Now.ToUniversalTime()) return false;
                }
                return true;
            }
        }

        private sealed class DoubleConstraintDescriptor
        {
            private ConstraintDescriptorFlags[] flagSets;
            private double[] values;

            public DoubleConstraintDescriptor(ConstraintDescriptorFlags[] flagSets, double[] values)
            {
                this.flagSets = flagSets;
                this.values = values;
            }

            public bool Check(double d)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    var flags = flagSets[i];
                    var value = values[i];
                    if (flags.HasFlag(ConstraintDescriptorFlags.MUST_MATCH_EXACTLY) && d != value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_MATCH) && d == value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BE_MORE_THAN) && d <= value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BE_MORE_THAN_OR_EQUAL_TO) && d < value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BE_LESS_THAN) && d >= value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BE_LESS_THAN_OR_EQUAL_TO) && d > value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_DIVIDE_BY) && Math.Abs(d % value) > double.Epsilon) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_DIVIDE_BY) && Math.Abs(d % value) <= double.Epsilon) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_CONTAIN_STRING) && !d.ToString().Contains(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_CONTAIN_STRING) && d.ToString().Contains(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BEGIN_WITH_STRING) && !d.ToString().StartsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_BEGIN_WITH_STRING) && d.ToString().StartsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_END_WITH_STRING) && !d.ToString().EndsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_END_WITH_STRING) && d.ToString().EndsWith(value.ToString())) return false;
                }
                return true;
            }
        }

        private sealed class LongConstraintDescriptor
        {
            private ConstraintDescriptorFlags[] flagSets;
            private long[] values;

            public LongConstraintDescriptor(ConstraintDescriptorFlags[] flagSets, long[] values)
            {
                this.flagSets = flagSets;
                this.values = values;
            }

            public bool Check(long l)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    var flags = flagSets[i];
                    var value = values[i];
                    if (flags.HasFlag(ConstraintDescriptorFlags.MUST_MATCH_EXACTLY) && l != value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_MATCH) && l == value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BE_MORE_THAN) && l <= value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BE_MORE_THAN_OR_EQUAL_TO) && l < value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BE_LESS_THAN) && l >= value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BE_LESS_THAN_OR_EQUAL_TO) && l > value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_DIVIDE_BY) && l % value != 0) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_DIVIDE_BY) && l % value == 0) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_CONTAIN_STRING) && !l.ToString().Contains(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_CONTAIN_STRING) && l.ToString().Contains(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BEGIN_WITH_STRING) && !l.ToString().StartsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_BEGIN_WITH_STRING) && l.ToString().StartsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_END_WITH_STRING) && !l.ToString().EndsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_END_WITH_STRING) && l.ToString().EndsWith(value.ToString())) return false;
                }
                return true;
            }
        }

        private sealed class StringConstraintDescriptor
        {
            private ConstraintDescriptorFlags[] flagSets;
            private string[] values;

            public StringConstraintDescriptor(ConstraintDescriptorFlags[] flagSets, string[] values)
            {
                this.flagSets = flagSets;
                this.values = values;
            }

            public bool Check(string l)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    var flags = flagSets[i];
                    var value = values[i];
                    if (flags.HasFlag(ConstraintDescriptorFlags.MUST_MATCH_EXACTLY) && l != value) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_MATCH) && l == value) return false;
                    // not handled: ConstraintDescriptorFlags.MUST_BE_MORE_THAN
                    // not handled: ConstraintDescriptorFlags.MUST_BE_MORE_THAN_OR_EQUAL_TO
                    // not handled: ConstraintDescriptorFlags.MUST_BE_LESS_THAN
                    // not handled: ConstraintDescriptorFlags.MUST_BE_LESS_THAN_OR_EQUAL_TO
                    // not handled: ConstraintDescriptorFlags.MUST_DIVIDE_BY
                    // not handled: ConstraintDescriptorFlags.MUST_NOT_DIVIDE_BY
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_CONTAIN_STRING) && !l.ToString().Contains(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_CONTAIN_STRING) && l.ToString().Contains(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_BEGIN_WITH_STRING) && !l.ToString().StartsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_BEGIN_WITH_STRING) && l.ToString().StartsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_END_WITH_STRING) && !l.ToString().EndsWith(value.ToString())) return false;
                    else if (flags.HasFlag(ConstraintDescriptorFlags.MUST_NOT_END_WITH_STRING) && l.ToString().EndsWith(value.ToString())) return false;
                }
                return true;
            }
        }

        [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
        public sealed class IsSmartDeserializable : Attribute
        {
            public IsSmartDeserializable() { }
        }

        protected sealed class DateTimeSpecialField : Attribute
        {
            public string FieldName { get; }
            public FieldDescriptorFlags Flags { get; }
            public DateTime DefaultValue { get; }
            private DateTimeConstraintDescriptor Constraints { get; }

            public DateTimeSpecialField(string fieldName, FieldDescriptorFlags flags = FieldDescriptorFlags.NONE, long defaultValue = 0,
                DateTimeConstraintDescriptorFlags[] cdFlags = null, long[] cdValues = null)
            {
                FieldName = fieldName;
                Flags = flags;
                DefaultValue = Util.GetDateTimeFromUnixTimestamp(defaultValue);
                if (cdFlags != null && cdValues != null)
                {
                    if (cdFlags.Length != cdValues.Length) throw new Exception("ConstraintDescriptor flags and values arrays must be of same length");
                    DateTime[] mCdValues = new DateTime[cdValues.Length];
                    for (int i = 0; i < cdValues.Length; i++) mCdValues[i] = Util.GetDateTimeFromUnixTimestamp(cdValues[i]);
                    Constraints = new DateTimeConstraintDescriptor(cdFlags, mCdValues);
                }
                else Constraints = new DateTimeConstraintDescriptor(new DateTimeConstraintDescriptorFlags[0], new DateTime[0]);
            }

            public bool ValueMatchesConstraints(DateTime d) => Constraints.Check(d);
        }

        [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
        protected sealed class DoubleBackedField : Attribute
        {
            public string FieldName { get; }
            public FieldDescriptorFlags Flags { get; }
            public double DefaultValue { get; }
            private DoubleConstraintDescriptor Constraints { get; }

            public DoubleBackedField(string fieldName, FieldDescriptorFlags flags = FieldDescriptorFlags.NONE, double defaultValue = 0,
                ConstraintDescriptorFlags[] cdFlags = null, double[] cdValues = null)
            {
                FieldName = fieldName;
                Flags = flags;
                DefaultValue = defaultValue;
                if (cdFlags != null && cdValues != null)
                {
                    if (cdFlags.Length != cdValues.Length) throw new Exception("ConstraintDescriptor flags and values arrays must be of same length");
                    Constraints = new DoubleConstraintDescriptor(cdFlags, cdValues);
                }
                else Constraints = new DoubleConstraintDescriptor(new ConstraintDescriptorFlags[0], new double[0]);
            }

            public bool ValueMatchesConstraints(double d) => Constraints.Check(d);
        }

        [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
        protected sealed class LongBackedField : Attribute
        {
            public string FieldName { get; }
            public FieldDescriptorFlags Flags { get; }
            public long DefaultValue { get; }
            private LongConstraintDescriptor Constraints { get; }

            public LongBackedField(string fieldName, FieldDescriptorFlags flags = FieldDescriptorFlags.NONE, long defaultValue = 0,
                ConstraintDescriptorFlags[] cdFlags = null, long[] cdValues = null)
            {
                FieldName = fieldName;
                Flags = flags;
                DefaultValue = defaultValue;
                if (cdFlags != null && cdValues != null)
                {
                    if (cdFlags.Length != cdValues.Length) throw new Exception("ConstraintDescriptor flags and values arrays must be of same length");
                    Constraints = new LongConstraintDescriptor(cdFlags, cdValues);
                }
                else Constraints = new LongConstraintDescriptor(new ConstraintDescriptorFlags[0], new long[0]);
            }

            public bool ValueMatchesConstraints(long l) => Constraints.Check(l);
        }

        [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
        protected sealed class StringBackedField : Attribute
        {
            public string FieldName { get; }
            public FieldDescriptorFlags Flags { get; }
            public string DefaultValue { get; }
            private StringConstraintDescriptor Constraints { get; }

            public StringBackedField(string fieldName, FieldDescriptorFlags flags = FieldDescriptorFlags.NONE, string defaultValue = null,
                ConstraintDescriptorFlags[] cdFlags = null, string[] cdValues = null)
            {
                FieldName = fieldName;
                Flags = flags;
                DefaultValue = defaultValue;
                if (cdFlags != null && cdValues != null)
                {
                    if (cdFlags.Length != cdValues.Length) throw new Exception("ConstraintDescriptor flags and values arrays must be of same length");
                    Constraints = new StringConstraintDescriptor(cdFlags, cdValues);
                }
                else Constraints = new StringConstraintDescriptor(new ConstraintDescriptorFlags[0], new string[0]);
            }

            public bool ValueMatchesConstraints(string s) => Constraints.Check(s);
        }

        [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
        protected sealed class DoubleDepotDictionaryField : Attribute
        {
            public string[] IncludedKeys { get; }
            private DoubleConstraintDescriptor Constraints { get; }
            public int priority;

            public DoubleDepotDictionaryField(string[] includedKeys = null, ConstraintDescriptorFlags[] cdFlags = null, double[] cdValues = null)
            {
                if (includedKeys != null) this.IncludedKeys = includedKeys;
                if (cdFlags != null && cdValues != null)
                {
                    if (cdFlags.Length != cdValues.Length) throw new Exception("ConstraintDescriptor flags and values arrays must be of same length");
                    Constraints = new DoubleConstraintDescriptor(cdFlags, cdValues);
                    if (includedKeys != null) priority = 1; else priority = -1;
                }
                else
                {
                    Constraints = new DoubleConstraintDescriptor(new ConstraintDescriptorFlags[0], new double[0]);
                    if (includedKeys != null) priority = 0; else priority = -1;
                }

            }
        }

        [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
        protected sealed class LongDepotDictionaryField : Attribute
        {
            public string[] IncludedKeys { get; }
            private LongConstraintDescriptor Constraints { get; }
            public int priority;

            public LongDepotDictionaryField(string[] includedKeys = null, ConstraintDescriptorFlags[] cdFlags = null, long[] cdValues = null)
            {
                if (includedKeys != null) this.IncludedKeys = includedKeys;
                if (cdFlags != null && cdValues != null)
                {
                    if (cdFlags.Length != cdValues.Length) throw new Exception("ConstraintDescriptor flags and values arrays must be of same length");
                    Constraints = new LongConstraintDescriptor(cdFlags, cdValues);
                    if (includedKeys != null) priority = 1; else priority = -1;
                }
                else
                {
                    Constraints = new LongConstraintDescriptor(new ConstraintDescriptorFlags[0], new long[0]);
                    if (includedKeys != null) priority = 0; else priority = -1;
                }
            }
        }

        [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
        protected sealed class StringDepotDictionaryField : Attribute
        {
            public string[] IncludedKeys { get; }
            private StringConstraintDescriptor Constraints { get; }
            public int priority;

            public StringDepotDictionaryField(string[] includedKeys = null, ConstraintDescriptorFlags[] cdFlags = null, string[] cdValues = null)
            {
                if (includedKeys != null) this.IncludedKeys = includedKeys;
                if (cdFlags != null && cdValues != null)
                {
                    if (cdFlags.Length != cdValues.Length) throw new Exception("ConstraintDescriptor flags and values arrays must be of same length");
                    Constraints = new StringConstraintDescriptor(cdFlags, cdValues);
                    if (includedKeys != null) priority = 1; else priority = -1;
                }
                else
                {
                    Constraints = new StringConstraintDescriptor(new ConstraintDescriptorFlags[0], new string[0]);
                    if (includedKeys != null) priority = 0; else priority = -1;
                }
            }
        }

        public string UniqueId { get; private set; }
        private Item nativeItem;

        private static T ToItemSchema<T>(Item nativeItem) where T : ItemSchema, new()
        {
            var t = new T();
            t.UniqueId = nativeItem.Id;
            t.nativeItem = nativeItem;
            t.PopulateFields();
            return t;
        }

        public static bool FitSchema<T>(Item nativeItem, out T item) where T : ItemSchema, new()
        {
            try
            {
                item = ToItemSchema<T>(nativeItem);
                return true;
            }
            catch (ItemSchemaException e) // ItemSchemaException is thrown if the item is a mismatch for the schema, so it's OK in this case
            {
                UnityEngine.Debug.LogWarning($"Item schema parse to {typeof(T)} failed!\n\n{e}");
                item = null;
                return false;
            }
        }

        private bool HasAttribute<T>(FieldInfo field, out T t) where T : Attribute
        {
            t = field.GetCustomAttribute<T>();
            return t != null;
        }

        private void PopulateFields()
        {
            var foundDoublesKeys = new List<string>();
            var foundLongsKeys = new List<string>();
            var foundStringsKeys = new List<string>();
            var ddFields = new List<FieldInfo>();
            var ldFields = new List<FieldInfo>();
            var sdFields = new List<FieldInfo>();
            var doublesDepots = new List<DoubleDepotDictionaryField>();
            var longsDepots = new List<LongDepotDictionaryField>();
            var stringsDepots = new List<StringDepotDictionaryField>();
            var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (HasAttribute(field, out DoubleBackedField dbf))
                {
                    double value = dbf.DefaultValue;
                    foundDoublesKeys.Add(dbf.FieldName);
                    var dbl = nativeItem.Doubles?.GetO(dbf.FieldName);
                    if (dbl != null) value = double.Parse(dbl);
                    else if (!dbf.Flags.HasFlag(FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND)) throw new ItemSchemaException($"Instance of ItemSchema type {GetType()} + was created for NativeItem {nativeItem.Id}" +
                        $" but the NativeItem's doubles dictionary doesn't contain key {dbf.FieldName} required for DoubleBackedField {field.Name} and DoubleBackedField doesn't have " +
                        $"FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND. This NativeItem cannot be mapped to the given schema.");
                    if (!dbf.ValueMatchesConstraints(value)) throw new ItemSchemaException($"Value of {value} for {dbf.FieldName} does not match constraints on field {field.Name}. " +
                        $"This NativeItem cannot be mapped to the given schema.");
                    if (field.FieldType == typeof(double)) field.SetValue(this, value);
                    else if (field.FieldType == typeof(float)) field.SetValue(this, Convert.ToSingle(value));
                    else throw new ItemSchemaException($"DoubleBackedField {field.Name} is of type {field.FieldType}, which double is not castable to. Please correct ItemSchema {GetType().Name}.");
                }
                else if (HasAttribute(field, out LongBackedField lbf))
                {
                    long value = lbf.DefaultValue;
                    foundLongsKeys.Add(lbf.FieldName);
                    var ln = nativeItem.Longs?.GetV(lbf.FieldName);
                    if (ln != null) value = ln.Value;
                    else if (!lbf.Flags.HasFlag(FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND)) throw new ItemSchemaException($"Instance of ItemSchema type {GetType()} + was created for NativeItem {nativeItem.Id}" +
                        $" but the NativeItem's longs dictionary doesn't contain key {lbf.FieldName} required for LongBackedField {field.Name} and LongBackedField doesn't have " +
                        $"FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND. This NativeItem cannot be mapped to the given schema.");
                    if (!lbf.ValueMatchesConstraints(value)) throw new ItemSchemaException($"Value of {value} for {lbf.FieldName} does not match constraints on field {field.Name}. " +
                        $"This NativeItem cannot be mapped to the given schema.");
                    if (field.FieldType == typeof(long)) field.SetValue(this, value);
                    else if (field.FieldType == typeof(int)) field.SetValue(this, Convert.ToInt32(value));
                    else if (field.FieldType == typeof(short)) field.SetValue(this, Convert.ToInt16(value));
                    else if (field.FieldType == typeof(byte)) field.SetValue(this, Convert.ToByte(value));
                    else throw new ItemSchemaException($"LongBackedField {field.Name} is of type {field.FieldType}, which long is not castable to. Please correct ItemSchema {GetType().Name}.");
                }
                else if (HasAttribute(field, out StringBackedField sbf))
                {
                    string value = sbf.DefaultValue;
                    foundStringsKeys.Add(sbf.FieldName);
                    var str = nativeItem.Strings?.GetO(sbf.FieldName);
                    if (str != null) value = str;
                    else if (!sbf.Flags.HasFlag(FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND)) throw new ItemSchemaException($"Instance of ItemSchema type {GetType()} + was created for NativeItem {nativeItem.Id}" +
                        $" but the NativeItem's strings dictionary doesn't contain key {sbf.FieldName} required for StringBackedField {field.Name} and StringBackedField doesn't have " +
                        $"FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND. This NativeItem cannot be mapped to the given schema.");
                    if (!sbf.ValueMatchesConstraints(value)) throw new ItemSchemaException($"Value of {value} for {sbf.FieldName} does not match constraints on field {field.Name}. " +
                        $"This NativeItem cannot be mapped to the given schema.");
                    if (field.FieldType == typeof(string) || field.FieldType == typeof(object)) field.SetValue(this, value);
                    else if ((field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) || field.FieldType.GetCustomAttribute<IsSmartDeserializable>() != null)
                        field.SetValue(this, JsonConvert.DeserializeObject(value, field.FieldType));
                    else throw new ItemSchemaException($"StringBackedField {field.Name} is of type {field.FieldType}, which string is not castable to. Please correct ItemSchema {GetType().Name}.");
                }
                else if (HasAttribute(field, out DoubleDepotDictionaryField dddf))
                {
                    doublesDepots.Add(dddf);
                    ddFields.Add(field);
                }
                else if (HasAttribute(field, out LongDepotDictionaryField lddf))
                {
                    longsDepots.Add(lddf);
                    ldFields.Add(field);
                }
                else if (HasAttribute(field, out StringDepotDictionaryField sddf))
                {
                    stringsDepots.Add(sddf);
                    sdFields.Add(field);
                }
                else if (HasAttribute(field, out DateTimeSpecialField dtsf))
                {
                    DateTime value = dtsf.DefaultValue;
                    foundLongsKeys.Add(dtsf.FieldName);
                    var ln = nativeItem.Longs.GetV(dtsf.FieldName);
                    if (ln != null) value = Util.GetDateTimeFromUnixTimestamp(ln.Value);
                    else if (!dtsf.Flags.HasFlag(FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND)) throw new ItemSchemaException($"Instance of ItemSchema type {GetType()} + was created for NativeItem {nativeItem.Id}" +
                        $" but the NativeItem's longs dictionary doesn't contain key {dtsf.FieldName} required for DateTimeSpecialField {field.Name} and DateTimeSpecialField doesn't have " +
                        $"FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND. This NativeItem cannot be mapped to the given schema.");
                    if (!dtsf.ValueMatchesConstraints(value)) throw new ItemSchemaException($"Value of {value} for {lbf.FieldName} does not match constraints on field {field.Name}. " +
                        $"This NativeItem cannot be mapped to the given schema.");
                    if (field.FieldType == typeof(DateTime)) field.SetValue(this, value);
                    else throw new ItemSchemaException($"LongBackedField {field.Name} is of type {field.FieldType}, which DateTime is not castable to. Please correct ItemSchema {GetType().Name}.");
                }
            }
            doublesDepots = doublesDepots.OrderByDescending(d => d.priority).ToList();
            longsDepots = longsDepots.OrderByDescending(d => d.priority).ToList();
            stringsDepots = stringsDepots.OrderByDescending(d => d.priority).ToList();
            foreach (var depot in doublesDepots)
            {
                var field = ddFields[doublesDepots.IndexOf(depot)];
                Dictionary<string, double> dict = new Dictionary<string, double>();
                foreach (var pair in nativeItem.Doubles)
                {
                    if (depot.IncludedKeys != null)
                    {
                        bool found = false;
                        foreach (var key in depot.IncludedKeys) if (key == pair.Key)
                            {
                                found = true;
                                break;
                            }
                        if (!found) continue; // Don't include this key in the dictionary
                    }
                    if (foundDoublesKeys.Contains(pair.Key)) continue;
                    dict.Add(pair.Key, double.Parse(pair.Value));
                    foundDoublesKeys.Add(pair.Key);
                }
                field.SetValue(this, dict);
            }
            foreach (var depot in longsDepots)
            {
                var field = ldFields[longsDepots.IndexOf(depot)];
                Dictionary<string, long> dict = new Dictionary<string, long>();
                foreach (var pair in nativeItem.Longs)
                {
                    if (depot.IncludedKeys != null)
                    {
                        bool found = false;
                        foreach (var key in depot.IncludedKeys) if (key == pair.Key)
                            {
                                found = true;
                                break;
                            }
                        if (!found) continue; // Don't include this key in the dictionary
                    }
                    if (foundLongsKeys.Contains(pair.Key)) continue;
                    dict.Add(pair.Key, pair.Value);
                    foundLongsKeys.Add(pair.Key);
                }
                field.SetValue(this, dict);
            }
            foreach (var depot in stringsDepots)
            {
                var field = sdFields[stringsDepots.IndexOf(depot)];
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (var pair in nativeItem.Strings)
                {
                    if (depot.IncludedKeys != null)
                    {
                        bool found = false;
                        foreach (var key in depot.IncludedKeys) if (key == pair.Key)
                            {
                                found = true;
                                break;
                            }
                        if (!found) continue; // Don't include this key in the dictionary
                    }
                    if (foundStringsKeys.Contains(pair.Key)) continue;
                    dict.Add(pair.Key, pair.Value);
                    foundStringsKeys.Add(pair.Key);
                }
                field.SetValue(this, dict);
            }
        }
    }
}