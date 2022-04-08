using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace Coral.Polyps
{
	public sealed class DObject : IEnumerable
	{
		[Flags]
		public enum TypeCode
		{
			DObject = 0x2,
			Boolean = 0x4,
			Char = 0x8,
			SByte = 0x10,
			Byte = 0x20,
			Int16 = 0x40,
			UInt16 = 0x80,
			Int32 = 0x100,
			UInt32 = 0x200,
			Int64 = 0x400,
			UInt64 = 0x800,
			Single = 0x1000,
			Double = 0x2000,
			Decimal = 0x4000,
			DateTime = 0x8000,
			TimeSpan = 0x10000,
			String = 0x20000,
			Guid = 0x40000,
			Array = 0x1,
			DObjectArray = 0x3,
			BooleanArray = 0x5,
			CharArray = 0x9,
			SByteArray = 0x11,
			ByteArray = 0x21,
			Int16Array = 0x41,
			UInt16Array = 0x81,
			Int32Array = 0x101,
			UInt32Array = 0x201,
			Int64Array = 0x401,
			UInt64Array = 0x801,
			SingleArray = 0x1001,
			DoubleArray = 0x2001,
			DecimalArray = 0x4001,
			DateTimeArray = 0x8001,
			TimeSpanArray = 0x10001,
			StringArray = 0x20001,
			GuidArray = 0x40001
		}

		[DebuggerDisplay("Entry [{Key} of {Type}]")]
		public sealed class Entry
		{
			public readonly string Key;

			public readonly object Value;

			public readonly TypeCode Type;

			public Entry(string key, object value, TypeCode type)
			{
				Key = key;
				Value = value;
				Type = type;
			}
		}

		private const int ExpandDelta = 4;

		private Entry[] entries;

		private int entriesCount;

		public int Count => entriesCount;

		public DObject()
		{
			entries = new Entry[4];
		}

		public DObject(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Значение вместительности должно быть неотрицательным.");
			}
			entries = new Entry[capacity];
		}

		public void Add(string key, DObject value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.DObject);
		}

		public void AddNull(string key)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, null, TypeCode.DObject);
		}

		public void Add(string key, bool value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Boolean);
		}

		public void Add(string key, char value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Char);
		}

		public void Add(string key, sbyte value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.SByte);
		}

		public void Add(string key, byte value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Byte);
		}

		public void Add(string key, short value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Int16);
		}

		public void Add(string key, ushort value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.UInt16);
		}

		public void Add(string key, int value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Int32);
		}

		public void Add(string key, uint value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.UInt32);
		}

		public void Add(string key, long value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Int64);
		}

		public void Add(string key, ulong value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.UInt64);
		}

		public void Add(string key, float value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Single);
		}

		public void Add(string key, double value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Double);
		}

		public void Add(string key, decimal value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Decimal);
		}

		public void Add(string key, DateTime value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.DateTime);
		}

		public void Add(string key, TimeSpan value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.TimeSpan);
		}

		public void Add(string key, string value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.String);
		}

		public void Add(string key, Guid value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Guid);
		}

		public void Add(string key, DObject[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.DObjectArray);
		}

		public void Add(string key, bool[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.BooleanArray);
		}

		public void Add(string key, char[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.CharArray);
		}

		public void Add(string key, sbyte[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.SByteArray);
		}

		public void Add(string key, byte[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.ByteArray);
		}

		public void Add(string key, short[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Int16Array);
		}

		public void Add(string key, ushort[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.UInt16Array);
		}

		public void Add(string key, int[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Int32Array);
		}

		public void Add(string key, uint[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.UInt32Array);
		}

		public void Add(string key, long[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.Int64Array);
		}

		public void Add(string key, ulong[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.UInt64Array);
		}

		public void Add(string key, float[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.SingleArray);
		}

		public void Add(string key, double[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.DoubleArray);
		}

		public void Add(string key, decimal[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.DecimalArray);
		}

		public void Add(string key, DateTime[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.DateTimeArray);
		}

		public void Add(string key, TimeSpan[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.TimeSpanArray);
		}

		public void Add(string key, string[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.StringArray);
		}

		public void Add(string key, Guid[] value)
		{
			if (entriesCount >= entries.Length)
			{
				Array.Resize(ref entries, entriesCount + 4);
			}
			entries[entriesCount++] = new Entry(key, value, TypeCode.GuidArray);
		}

		public static Entry GetEntry(DObject map, int index)
		{
			return map.entries[index];
		}

		public static TValue GetEntryValue<TValue>(DObject map, string key)
		{
			Entry entry = map.entries.FirstOrDefault((Entry e) => e.Key == key);
			if (entry != null)
			{
				return (TValue)entry.Value;
			}
			return default(TValue);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			for (int i = 0; i < entriesCount; i++)
			{
				yield return entries[i];
			}
		}
	}
}
