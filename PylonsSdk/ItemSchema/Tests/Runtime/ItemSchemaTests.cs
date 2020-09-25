using System;
using System.Collections.Generic;

using NUnit.Framework;
using Newtonsoft.Json;
using PylonsSdk.Tx;
using UnityEngine;

namespace PylonsSdk.ItemSchema.Tests
{
    public class ItemSchemaTests
    {
#pragma warning disable CS0649
        [ItemSchema.IsSmartDeserializable]
        private class TestSubstructure
        {
            public int AnInt;
            public int AnotherInt;
            public string AString;
        }

        private class TestSchemaWithDateTime : ItemSchema
        {
            [DateTimeSpecialField("timestamp")]
            public DateTime timestamp;

            public static Item GetNativeItem()
            {
                Item nativeItem = new Item("", "basf", new KeyValuePair<string, string>[0], new KeyValuePair<string, long>[] { new KeyValuePair<string, long>("timestamp", 0) }, 
                    new KeyValuePair<string, string>[0], "", "", "", "", true, 0, 0);
                return nativeItem;
            }
        }

        private class TestSchemaWithDepots : ItemSchema
        {
            [StringBackedField("specific")]
            public string SomethingSpecific;
            [StringDepotDictionaryField(new string[] { "bullshit", "moreBullshit" })]
            public Dictionary<string, string> Bullshit;
            [StringDepotDictionaryField]
            public Dictionary<string, string> EverythingElse;

            public static Item GetNativeItem()
            {
                Item nativeItem = new Item("", "basf", new KeyValuePair<string, string>[0], new KeyValuePair<string, long>[] { new KeyValuePair<string, long>("timestamp", 0) },
                    new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("specific", "this is specific indeed"),
                        new KeyValuePair<string, string>("bullshit", "only a lil bullshit"),
                        new KeyValuePair<string, string>("blah", "blah"),
                        new KeyValuePair<string, string>("foo", "bar")
                    }, "", "", "", "", true, 0, 0);
                return nativeItem;
            }
        }

        /// <summary>
        /// This exists to provide safety against weird behavior w/ attributes on inherited fields.
        /// It doesn't need to actually _extend_ the functionality of vanilla TestSchemaPass in any way;
        /// it just needs to be a subclass at all.
        /// </summary>
        private class TestSchemaPassExtend : TestSchemaPass
        {
            public const string lol = "whatever";
        }

        private class TestSchemaPass : ItemSchema
        {
            [DoubleBackedField("dFID")]
            public double doubleField_IsDouble;
            [DoubleBackedField("dFND")]
            public float doubleField_NotDouble;
            [DoubleBackedField("NOT THERE", FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND, double.NaN)]
            public double doubleField_Defaults;
            [LongBackedField("lFIL")]
            public long longField_IsLong;
            [LongBackedField("lFNL")]
            public short longField_NotLong;
            [LongBackedField("NOT THERE", FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND, 1024)]
            public long longField_Defaults;
            [StringBackedField("sFIS")]
            public string stringField_IsString;
            [StringBackedField("sFNS")]
            public object stringField_NotString;
            [StringBackedField("NOT THERE", FieldDescriptorFlags.DEFAULT_VALUE_IF_NOT_FOUND, "Nope.")]
            public string stringField_Defaults;
            [StringBackedField("sFSD")]
            public TestSubstructure stringField_SerializedData;
            [LongBackedField("lFWC", FieldDescriptorFlags.NONE, 0, 
                new ConstraintDescriptorFlags[] {ConstraintDescriptorFlags.MUST_MATCH_EXACTLY}, new long[] { 42 })]
            public long longBackedField_WithConstraints;

            public static Item GetNativeItemPassing (out TestSubstructure testSubstructure)
            {
                testSubstructure = new TestSubstructure
                {
                    AnInt = 99,
                    AnotherInt = 910,
                    AString = "lol"
                };

                Item nativeItem = new Item
                    (
                        id: "",
                        cookbookId: "",
                        nodeVersion: "1.0",
                        sender: "",
                        ownerRecipeId: "",
                        ownerTradeId: "",
                        tradable: true,
                        lastUpdate: 0,
                        transferFee: 0,
                        doubles: new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("dFID", double.Epsilon.ToString()),
                            new KeyValuePair<string, string>("dFND", double.NegativeInfinity.ToString())
                        },
                        longs: new KeyValuePair<string, long>[]
                        {
                            new KeyValuePair<string, long>("lFIL", 101),
                            new KeyValuePair<string, long>("lFNL", 999),
                            new KeyValuePair<string, long>("lFWC", 42) // 42 matches constraints, so this should map to the schema (it wouldn't otherwise)
                        },
                        strings: new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("sFIS", "This is a string!"),
                            new KeyValuePair<string, string>("sFNS", "This is a string, but it's also an object."),
                            new KeyValuePair<string, string>("sFSD", JsonConvert.SerializeObject(testSubstructure))
                        }
                    );
                return nativeItem;
            }

            public static Item GetNativeItemPassingWithOptionals(out TestSubstructure testSubstructure)
            {
                testSubstructure = new TestSubstructure
                {
                    AnInt = 99,
                    AnotherInt = 910,
                    AString = "lol"
                };
                Item nativeItem = new Item
                    (
                        id: "",
                        cookbookId: "",
                        nodeVersion: "1.0",
                        sender: "",
                        ownerRecipeId: "",
                        ownerTradeId: "",
                        tradable: true,
                        lastUpdate: 0,
                        transferFee: 0,
                        doubles: new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("dFID", double.Epsilon.ToString()),
                            new KeyValuePair<string, string>("dFND", double.NegativeInfinity.ToString()),
                            new KeyValuePair<string, string>("NOT THERE", 1.ToString())
                        },
                        longs: new KeyValuePair<string, long>[]
                        {
                            new KeyValuePair<string, long>("lFIL", 101),
                            new KeyValuePair<string, long>("lFNL", 999),
                            new KeyValuePair<string, long>("lFWC", 42), // 42 matches constraints, so this should map to the schema (it wouldn't otherwise)
                            new KeyValuePair<string, long>("NOT THERE", 2)
                        },
                        strings: new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("sFIS", "This is a string!"),
                            new KeyValuePair<string, string>("sFNS", "This is a string, but it's also an object."),
                            new KeyValuePair<string, string>("sFSD", JsonConvert.SerializeObject(testSubstructure)),
                            new KeyValuePair<string, string>("NOT THERE", "yep.")
                        }
                    );
                return nativeItem;
            }

            public static Item GetNativeItemNotPassing (out TestSubstructure testSubstructure)
            {
                testSubstructure = new TestSubstructure
                {
                    AnInt = 99,
                    AnotherInt = 910,
                    AString = "lol"
                };
                Item nativeItem = new Item
                    (
                        id: "",
                        cookbookId: "",
                        nodeVersion: "1.0",
                        sender: "",
                        ownerRecipeId: "",
                        ownerTradeId: "",
                        tradable: true,
                        lastUpdate: 0,
                        transferFee: 0,
                        doubles: new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("dFID", double.Epsilon.ToString()),
                            new KeyValuePair<string, string>("dFND", double.NegativeInfinity.ToString())
                        },
                        longs: new KeyValuePair<string, long>[]
                        {
                            new KeyValuePair<string, long>("lFIL", 101),
                            new KeyValuePair<string, long>("lFNL", 999),
                            new KeyValuePair<string, long>("lFWC", 420) // 420 doesn't match constraints, so this shouldn't map to the schema even though the structure is sound
                        },
                        strings: new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("sFIS", "This is a string!"),
                            new KeyValuePair<string, string>("sFNS", "This is a string, but it's also an object."),
                            new KeyValuePair<string, string>("sFSD", JsonConvert.SerializeObject(testSubstructure))
                        }
                    );
                return nativeItem;
            }
        }
#pragma warning restore CS0649

        [Test]
        public void ItemSchemaTest_CorrectValuesAfterConversionToSchema()
        {
            var nativeItem = TestSchemaPass.GetNativeItemPassing(out var testSubstructure);
            ItemSchema.FitSchema<TestSchemaPass>(nativeItem, out var schematized);
            Assert.AreEqual(nativeItem.Id, schematized.UniqueId);
            Assert.AreEqual(double.Parse(nativeItem.Doubles.GetO("dFID")), schematized.doubleField_IsDouble);
            Assert.AreEqual(float.Parse(nativeItem.Doubles.GetO("dFND")), schematized.doubleField_NotDouble);
            Assert.AreEqual(double.NaN, schematized.doubleField_Defaults);
            Assert.AreEqual(nativeItem.Longs.GetV("lFIL"), schematized.longField_IsLong);
            Assert.AreEqual((short)nativeItem.Longs.GetV("lFNL"), schematized.longField_NotLong);
            Assert.AreEqual(1024, schematized.longField_Defaults);
            Assert.AreEqual(nativeItem.Strings.GetO("sFIS"), schematized.stringField_IsString);
            Assert.AreEqual(nativeItem.Strings.GetO("sFNS"), (string)schematized.stringField_NotString);
            Assert.AreEqual("Nope.", schematized.stringField_Defaults);
            Assert.AreEqual(testSubstructure.AnInt, schematized.stringField_SerializedData.AnInt);
            Assert.AreEqual(testSubstructure.AnotherInt, schematized.stringField_SerializedData.AnotherInt);
            Assert.AreEqual(testSubstructure.AString, schematized.stringField_SerializedData.AString);
            Assert.AreEqual(nativeItem.Longs.GetV("lFWC"), schematized.longBackedField_WithConstraints);
        }

        [Test]
        public void ItemSchemaTest_CorrectValuesAfterConversionToSchema_Inheritance()
        {
            var nativeItem = TestSchemaPass.GetNativeItemPassing(out var testSubstructure);
            ItemSchema.FitSchema<TestSchemaPassExtend>(nativeItem, out var schematized);
            Assert.AreEqual(nativeItem.Id, schematized.UniqueId);
            Assert.AreEqual(double.Parse(nativeItem.Doubles.GetO("dFID")), schematized.doubleField_IsDouble);
            Assert.AreEqual(float.Parse(nativeItem.Doubles.GetO("dFND")), schematized.doubleField_NotDouble);
            Assert.AreEqual(double.NaN, schematized.doubleField_Defaults);
            Assert.AreEqual(nativeItem.Longs.GetV("lFIL"), schematized.longField_IsLong);
            Assert.AreEqual((short)nativeItem.Longs.GetV("lFNL"), schematized.longField_NotLong);
            Assert.AreEqual(1024, schematized.longField_Defaults);
            Assert.AreEqual(nativeItem.Strings.GetO("sFIS"), schematized.stringField_IsString);
            Assert.AreEqual(nativeItem.Strings.GetO("sFNS"), (string)schematized.stringField_NotString);
            Assert.AreEqual("Nope.", schematized.stringField_Defaults);
            Assert.AreEqual(testSubstructure.AnInt, schematized.stringField_SerializedData.AnInt);
            Assert.AreEqual(testSubstructure.AnotherInt, schematized.stringField_SerializedData.AnotherInt);
            Assert.AreEqual(testSubstructure.AString, schematized.stringField_SerializedData.AString);
            Assert.AreEqual(nativeItem.Longs.GetV("lFWC"), schematized.longBackedField_WithConstraints);
        }


        [Test]
        public void ItemSchemaTest_OptionalFieldsHaveCorrectValues ()
        {
            var nativeItem = TestSchemaPass.GetNativeItemPassingWithOptionals(out var testSubstructure);
            ItemSchema.FitSchema<TestSchemaPass>(nativeItem, out var schematized);
            Assert.AreEqual(double.Parse(nativeItem.Doubles.GetO("NOT THERE")), schematized.doubleField_Defaults);
            Assert.AreEqual(nativeItem.Longs.GetV("NOT THERE"), schematized.longField_Defaults);
            Assert.AreEqual(nativeItem.Strings.GetO("NOT THERE"), schematized.stringField_Defaults);
        }

        [Test]
        public void ItemSchemaTest_MatchesSchema ()
        {
            Assert.IsTrue(ItemSchema.FitSchema<TestSchemaPass>(TestSchemaPass.GetNativeItemPassing(out _), out _));
            Assert.IsTrue(ItemSchema.FitSchema<TestSchemaPass>(TestSchemaPass.GetNativeItemPassingWithOptionals(out _), out _));
        }

        [Test]
        public void ItemSchemaTest_DoesNotMatchSchema ()
        {
            Debug.Log("Two schema matches are going to fail as part of ItemSchemaTest_DoesNotMatchSchema now. The warnings are expected.");
            Assert.IsFalse(ItemSchema.FitSchema<TestSchemaPass>(new Item(), out _));
            Assert.IsFalse(ItemSchema.FitSchema<TestSchemaPass>(TestSchemaPass.GetNativeItemNotPassing(out _), out _));
        }

        [Test]
        public void ItemSchemaTest_Depots ()
        {
            ItemSchema.FitSchema<TestSchemaWithDepots>(TestSchemaWithDepots.GetNativeItem(), out var schematized);
            Assert.AreEqual("this is specific indeed", schematized.SomethingSpecific);
            Assert.AreEqual(1, schematized.Bullshit.Count);
            Assert.AreEqual("only a lil bullshit", schematized.Bullshit["bullshit"]);
            Assert.AreEqual(2, schematized.EverythingElse.Count);
            Assert.AreEqual("bar", schematized.EverythingElse["foo"]);
            Assert.AreEqual("blah", schematized.EverythingElse["blah"]);
        }

        [Test]
        public void ItemSchemaTest_DateTime ()
        {
            ItemSchema.FitSchema<TestSchemaWithDateTime>(TestSchemaWithDateTime.GetNativeItem(), out var schematized);
            Assert.AreEqual(Util.UnixEpoch, schematized.timestamp);
        }
    }
}