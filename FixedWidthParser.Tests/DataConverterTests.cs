﻿// Copyright (c) 2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using NUnit.Framework;
using SoftCircuits.Parsers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FixedWidthParserTests
{
    public class DataConverterTests
    {
        private class AllTypesClass
        {
            [FixedWidthField(10)]
            public Boolean BooleanValue { get; set; }
            [FixedWidthField(10)]
            public Byte ByteValue { get; set; }
            [FixedWidthField(10)]
            public Char CharValue { get; set; }
            [FixedWidthField(10)]
            public Decimal DecimalValue { get; set; }
            [FixedWidthField(10)]
            public Double DoubleValue { get; set; }
            [FixedWidthField(36)]
            public Guid GuidValue { get; set; }
            [FixedWidthField(10)]
            public Int16 Int16Value { get; set; }
            [FixedWidthField(10)]
            public Int32 Int32Value { get; set; }
            [FixedWidthField(10)]
            public Int64 Int64Value { get; set; }
            [FixedWidthField(10)]
            public SByte SByteValue { get; set; }
            [FixedWidthField(10)]
            public Single SingleValue { get; set; }
            [FixedWidthField(10)]
            public String StringValue { get; set; }
            [FixedWidthField(10)]
            public UInt16 UInt16Value { get; set; }
            [FixedWidthField(10)]
            public UInt32 UInt32Value { get; set; }
            [FixedWidthField(10)]
            public UInt64 UInt64Value { get; set; }
        }

        private class AllTypesComparer : IComparer, IComparer<AllTypesClass>
        {
            public int Compare(object a, object b)
            {
                if (!(a is AllTypesClass ta) || !(b is AllTypesClass tb))
                    throw new InvalidOperationException();
                return Compare(ta, tb);
            }

            public int Compare([AllowNull] AllTypesClass a, [AllowNull] AllTypesClass b)
            {
                int result;

                result = a.BooleanValue.CompareTo(b.BooleanValue);
                if (result != 0) return result;
                result = a.ByteValue.CompareTo(b.ByteValue);
                if (result != 0) return result;
                result = a.CharValue.CompareTo(b.CharValue);
                if (result != 0) return result;
                result = a.DecimalValue.CompareTo(b.DecimalValue);
                if (result != 0) return result;
                result = a.DoubleValue.CompareTo(b.DoubleValue);
                if (result != 0) return result;
                result = a.GuidValue.CompareTo(b.GuidValue);
                if (result != 0) return result;
                result = a.Int16Value.CompareTo(b.Int16Value);
                if (result != 0) return result;
                result = a.Int32Value.CompareTo(b.Int32Value);
                if (result != 0) return result;
                result = a.Int64Value.CompareTo(b.Int64Value);
                if (result != 0) return result;
                result = a.SByteValue.CompareTo(b.SByteValue);
                if (result != 0) return result;
                result = a.SingleValue.CompareTo(b.SingleValue);
                if (result != 0) return result;
                result = a.StringValue.CompareTo(b.StringValue);
                if (result != 0) return result;
                result = a.UInt16Value.CompareTo(b.UInt16Value);
                if (result != 0) return result;
                result = a.UInt32Value.CompareTo(b.UInt32Value);
                if (result != 0) return result;
                result = a.UInt64Value.CompareTo(b.UInt64Value);
                return result;
            }
        }

        private readonly List<AllTypesClass> AllTypesItems = new List<AllTypesClass>
        {
            new AllTypesClass
            {
                BooleanValue = true,
                ByteValue = 47,
                CharValue = 'r',
                DecimalValue = 123.456m,
                DoubleValue = 47.9,
                GuidValue = Guid.NewGuid(),
                Int16Value = 4887,
                Int32Value = -98072,
                Int64Value = 489938827,
                SByteValue = -87,
                SingleValue = 432.99f,
                StringValue = "abcdef",
                UInt16Value = 8402,
                UInt32Value = 4662900,
                UInt64Value = 650094891,
            },
            new AllTypesClass
            {
                BooleanValue = false,
                ByteValue = 107,
                CharValue = 'v',
                DecimalValue = 988.22m,
                DoubleValue = 90.44,
                GuidValue = Guid.NewGuid(),
                Int16Value = -987,
                Int32Value = 98072,
                Int64Value = -489938827,
                SByteValue = 87,
                SingleValue = 456.1f,
                StringValue = "xyz",
                UInt16Value = 44987,
                UInt32Value = 472209,
                UInt64Value = 7760982,
            },
            new AllTypesClass
            {
                BooleanValue = true,
                ByteValue = 98,
                CharValue = '4',
                DecimalValue = 780.2m,
                DoubleValue = 86.9,
                GuidValue = Guid.NewGuid(),
                Int16Value = -4721,
                Int32Value = 18692,
                Int64Value = 84452091,
                SByteValue = 30,
                SingleValue = -98.4f,
                StringValue = "",
                UInt16Value = 44079,
                UInt32Value = 440796,
                UInt64Value = 4407960,
            },
            new AllTypesClass
            {
                BooleanValue = false,
                ByteValue = 142,
                CharValue = '&',
                DecimalValue = 9088261.4m,
                DoubleValue = 478.32,
                GuidValue = Guid.NewGuid(),
                Int16Value = -1880,
                Int32Value = 45661,
                Int64Value = -43811297,
                SByteValue = -7,
                SingleValue = 28.28f,
                StringValue = "border",
                UInt16Value = 42660,
                UInt32Value = 4266079,
                UInt64Value = 426607980,
            },
        };

        [Test]
        public void TestIntrinsicDataConverters()
        {
            ObjectMappingTests x = new ObjectMappingTests();

            x.WriteReadValues(AllTypesItems, out List<AllTypesClass> results);
            CollectionAssert.AreEqual(AllTypesItems, results, new AllTypesComparer());
        }

        private class Person
        {
            [FixedWidthField(8)]
            public int Id { get; set; }
            [FixedWidthField(12)]
            public string FirstName { get; set; }
            [FixedWidthField(12)]
            public string LastName { get; set; }
            [FixedWidthField(12, ConverterType = typeof(BirthdateConverter))]
            public DateTime Birthdate { get; set; }
        }

        private class PersonComparer : IComparer, IComparer<Person>
        {
            public int Compare(object a, object b)
            {
                if (!(a is Person ta) || !(b is Person tb))
                    throw new InvalidOperationException();
                return Compare(ta, tb);
            }

            public int Compare([AllowNull] Person a, [AllowNull] Person b)
            {
                int result;

                result = a.Id.CompareTo(b.Id);
                if (result != 0) return result;
                result = a.FirstName.CompareTo(b.FirstName);
                if (result != 0) return result;
                result = a.LastName.CompareTo(b.LastName);
                if (result != 0) return result;
                result = a.Birthdate.CompareTo(b.Birthdate);
                return result;
            }
        }

        private class BirthdateConverter : DataConverter<DateTime>
        {
            private const string Format = "yyyyMMdd";

            public override string ConvertToString(DateTime value) => value.ToString(Format);

            public override bool TryConvertFromString(string s, out DateTime value)
            {
                return DateTime.TryParseExact(s, Format, null, System.Globalization.DateTimeStyles.None, out value);
            }
        }

        private readonly List<Person> People = new List<Person>
        {
            new Person { Id = 1, FirstName = "Bill", LastName = "Smith", Birthdate = new DateTime(1982, 2, 7) },
            new Person { Id = 1, FirstName = "Gary", LastName = "Parker", Birthdate = new DateTime(1989, 8, 2) },
            new Person { Id = 1, FirstName = "Karen", LastName = "Wilson", Birthdate = new DateTime(1978, 6, 24) },
            new Person { Id = 1, FirstName = "Jeff", LastName = "Johnson", Birthdate = new DateTime(1972, 4, 18) },
            new Person { Id = 1, FirstName = "John", LastName = "Carter", Birthdate = new DateTime(1982, 12, 21) },
        };

        [Test]
        public void TestCustomConverter()
        {
            ObjectMappingTests x = new ObjectMappingTests();

            x.WriteReadValues(People, out List<Person> results);
            CollectionAssert.AreEqual(People, results, new PersonComparer());
        }
    }
}
