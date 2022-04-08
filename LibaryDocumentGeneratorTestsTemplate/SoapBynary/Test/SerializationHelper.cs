using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Coral.Polyps;

namespace Coral.Communication.EzCom.NamedPipe
{
	public static class SerializationHelper
	{
		private static class ZeroLengthArrays
		{
			public static readonly DObject[] DObject = new DObject[0];

			public static readonly bool[] Boolean = new bool[0];

			public static readonly char[] Char = new char[0];

			public static readonly sbyte[] SByte = new sbyte[0];

			public static readonly byte[] Byte = new byte[0];

			public static readonly short[] Int16 = new short[0];

			public static readonly ushort[] UInt16 = new ushort[0];

			public static readonly int[] Int32 = new int[0];

			public static readonly uint[] UInt32 = new uint[0];

			public static readonly long[] Int64 = new long[0];

			public static readonly ulong[] UInt64 = new ulong[0];

			public static readonly float[] Single = new float[0];

			public static readonly double[] Double = new double[0];

			public static readonly decimal[] Decimal = new decimal[0];

			public static readonly DateTime[] DateTime = new DateTime[0];

			public static readonly TimeSpan[] TimeSpan = new TimeSpan[0];

			public static readonly string[] String = new string[0];

			public static readonly Guid[] Guid = new Guid[0];
		}

		private sealed class SerializationScope
		{
			public readonly object Instance;

			public int Index;

			public readonly DObject.TypeCode Type;

			public SerializationScope(DObject dObject)
			{
				Instance = dObject;
				Type = DObject.TypeCode.DObject;
			}

			public SerializationScope(Array array, DObject.TypeCode type)
			{
				Instance = array;
				Type = type;
			}
		}

		private sealed class DeserializationScope
		{
			public object Instance;

			public readonly DObject.TypeCode Type;

			public int Index;

			public string LastKey;

			public readonly int Count;

			public DeserializationScope(DObject dObject, int count)
			{
				Instance = dObject;
				Type = DObject.TypeCode.DObject;
				Count = count;
			}

			public DeserializationScope(List<object> list, DObject.TypeCode type, int count)
			{
				Instance = list;
				Type = type;
				Count = count;
			}
		}

		public static void Write(BinaryWriter writer, DObject.TypeCode type, object value)
		{
			writer.Write((int)type);
			if (WriteIfPrimitiveOrNull(writer, type, value, out var scope))
			{
				return;
			}
			Stack<SerializationScope> stack = new Stack<SerializationScope>();
			while (true)
			{
				int num;
				SerializationScope scope2;
				int num2;
				SerializationScope scope3;
				if (scope.Type == DObject.TypeCode.DObject)
				{
					DObject dObject = (DObject)scope.Instance;
					int count = dObject.Count;
					num = scope.Index;
					while (num < count)
					{
						DObject.Entry entry = DObject.GetEntry(dObject, num);
						writer.Write(entry.Key);
						writer.Write((int)entry.Type);
						if (WriteIfPrimitiveOrNull(writer, entry.Type, entry.Value, out scope2))
						{
							num++;
							continue;
						}
						goto IL_007c;
					}
				}
				else
				{
					Array array = (Array)scope.Instance;
					int length = array.Length;
					DObject.TypeCode type2 = scope.Type & ~DObject.TypeCode.Array;
					num2 = scope.Index;
					while (num2 < length)
					{
						object value2 = array.GetValue(num2);
						if (WriteIfPrimitiveOrNull(writer, type2, value2, out scope3))
						{
							num2++;
							continue;
						}
						goto IL_00e3;
					}
				}
				if (stack.Count >= 1)
				{
					scope = stack.Pop();
					continue;
				}
				break;
			IL_00e3:
				scope.Index = num2 + 1;
				stack.Push(scope);
				scope = scope3;
				continue;
			IL_007c:
				scope.Index = num + 1;
				stack.Push(scope);
				scope = scope2;
			}
		}

		private static bool WriteIfPrimitiveOrNull(BinaryWriter writer, DObject.TypeCode type, object value, out SerializationScope scope)
		{
			if (type != DObject.TypeCode.DObject && !type.HasFlag(DObject.TypeCode.Array))
			{
				scope = null;
				switch (type)
				{
					case DObject.TypeCode.Boolean:
						writer.Write((bool)value);
						return true;
					case DObject.TypeCode.Char:
						writer.Write((char)value);
						return true;
					case DObject.TypeCode.SByte:
						writer.Write((sbyte)value);
						return true;
					case DObject.TypeCode.Byte:
						writer.Write((byte)value);
						return true;
					case DObject.TypeCode.Int16:
						writer.Write((short)value);
						return true;
					case DObject.TypeCode.UInt16:
						writer.Write((ushort)value);
						return true;
					case DObject.TypeCode.Int32:
						writer.Write((int)value);
						return true;
					case DObject.TypeCode.UInt32:
						writer.Write((uint)value);
						return true;
					case DObject.TypeCode.Int64:
						writer.Write((long)value);
						return true;
					case DObject.TypeCode.UInt64:
						writer.Write((ulong)value);
						return true;
					case DObject.TypeCode.Single:
						writer.Write((float)value);
						return true;
					case DObject.TypeCode.Double:
						writer.Write((double)value);
						return true;
					case DObject.TypeCode.Decimal:
						writer.Write((decimal)value);
						return true;
					case DObject.TypeCode.DateTime:
						writer.Write(((DateTime)value).ToBinary());
						return true;
					case DObject.TypeCode.TimeSpan:
						writer.Write(((TimeSpan)value).Ticks);
						return true;
					case DObject.TypeCode.String:
						if (value == null)
						{
							writer.Write((byte)0);
						}
						else
						{
							writer.Write((byte)1);
							writer.Write((string)value);
						}
						return true;
					case DObject.TypeCode.Guid:
						writer.Write(((Guid)value).ToByteArray());
						return true;
					default:
						throw new ArgumentOutOfRangeException("type", type, null);
				}
			}
			if (value == null)
			{
				writer.Write(-1);
				scope = null;
				return true;
			}
			if (type == DObject.TypeCode.DObject)
			{
				DObject dObject = (DObject)value;
				if (dObject.Count < 1)
				{
					writer.Write(0);
					scope = null;
					return true;
				}
				writer.Write(dObject.Count);
				scope = new SerializationScope(dObject);
				return false;
			}
			Array array = (Array)value;
			if (array.Length < 1)
			{
				writer.Write(0);
				scope = null;
				return true;
			}
			writer.Write(array.Length);
			scope = new SerializationScope(array, type);
			return false;
		}

		public static void Read(BinaryReader reader, out DObject.TypeCode type, out object value)
		{
			type = (DObject.TypeCode)reader.ReadInt32();
			if (ReadIfPrimitiveOrNull(reader, type, out value, out var scope))
			{
				return;
			}
			Stack<DeserializationScope> stack = new Stack<DeserializationScope>();
			while (true)
			{
				int num;
				string text;
				DeserializationScope scope2;
				int num2;
				if (scope.Type == DObject.TypeCode.DObject)
				{
					DObject obj = (DObject)scope.Instance;
					int count = scope.Count;
					num = scope.Index;
					while (num < count)
					{
						text = reader.ReadString();
						DObject.TypeCode type2 = (DObject.TypeCode)reader.ReadInt32();
						if (ReadIfPrimitiveOrNull(reader, type2, out var value2, out scope2))
						{
							AddToDObject(obj, text, type2, value2);
							num++;
							continue;
						}
						goto IL_0071;
					}
				}
				else
				{
					List<object> list = (List<object>)scope.Instance;
					int count2 = scope.Count;
					DObject.TypeCode typeCode = scope.Type & ~DObject.TypeCode.Array;
					num2 = scope.Index;
					while (num2 < count2)
					{
						if (ReadIfPrimitiveOrNull(reader, typeCode, out var value3, out scope2))
						{
							list.Add(value3);
							num2++;
							continue;
						}
						goto IL_00df;
					}
					scope.Instance = ListToTypedArray(list, typeCode);
				}
				if (stack.Count < 1)
				{
					break;
				}
				DeserializationScope deserializationScope = scope;
				scope = stack.Pop();
				if (scope.Type == DObject.TypeCode.DObject)
				{
					AddToDObject((DObject)scope.Instance, scope.LastKey, deserializationScope.Type, deserializationScope.Instance);
				}
				else
				{
					((List<object>)scope.Instance).Add(deserializationScope.Instance);
				}
				continue;
			IL_0071:
				scope.Index = num + 1;
				scope.LastKey = text;
				stack.Push(scope);
				scope = scope2;
				continue;
			IL_00df:
				scope.Index = num2 + 1;
				stack.Push(scope);
				scope = scope2;
			}
			value = scope.Instance;
		}

		private static bool ReadIfPrimitiveOrNull(BinaryReader reader, DObject.TypeCode type, out object value, out DeserializationScope scope)
		{
			if (type != DObject.TypeCode.DObject && !type.HasFlag(DObject.TypeCode.Array))
			{
				scope = null;
				switch (type)
				{
					case DObject.TypeCode.Boolean:
						value = reader.ReadBoolean();
						return true;
					case DObject.TypeCode.Char:
						value = reader.ReadChar();
						return true;
					case DObject.TypeCode.SByte:
						value = reader.ReadSByte();
						return true;
					case DObject.TypeCode.Byte:
						value = reader.ReadByte();
						return true;
					case DObject.TypeCode.Int16:
						value = reader.ReadInt16();
						return true;
					case DObject.TypeCode.UInt16:
						value = reader.ReadUInt16();
						return true;
					case DObject.TypeCode.Int32:
						value = reader.ReadInt32();
						return true;
					case DObject.TypeCode.UInt32:
						value = reader.ReadUInt32();
						return true;
					case DObject.TypeCode.Int64:
						value = reader.ReadInt64();
						return true;
					case DObject.TypeCode.UInt64:
						value = reader.ReadUInt64();
						return true;
					case DObject.TypeCode.Single:
						value = reader.ReadSingle();
						return true;
					case DObject.TypeCode.Double:
						value = reader.ReadDouble();
						return true;
					case DObject.TypeCode.Decimal:
						value = reader.ReadDecimal();
						return true;
					case DObject.TypeCode.DateTime:
						value = DateTime.FromBinary(reader.ReadInt64());
						return true;
					case DObject.TypeCode.TimeSpan:
						value = TimeSpan.FromTicks(reader.ReadInt64());
						return true;
					case DObject.TypeCode.String:
						{
							byte b = reader.ReadByte();
							value = ((b == 0) ? null : reader.ReadString());
							return true;
						}
					case DObject.TypeCode.Guid:
						value = new Guid(reader.ReadBytes(16));
						return true;
					default:
						throw new ArgumentOutOfRangeException("type", type, null);
				}
			}
			int num = reader.ReadInt32();
			if (num < 0)
			{
				value = null;
				scope = null;
				return true;
			}
			if (num < 1)
			{
				scope = null;
				if (type == DObject.TypeCode.DObject)
				{
					value = new DObject();
					return true;
				}
				switch (type & ~DObject.TypeCode.Array)
				{
					case DObject.TypeCode.DObject:
						value = ZeroLengthArrays.DObject;
						return true;
					case DObject.TypeCode.Boolean:
						value = ZeroLengthArrays.Boolean;
						return true;
					case DObject.TypeCode.Char:
						value = ZeroLengthArrays.Char;
						return true;
					case DObject.TypeCode.SByte:
						value = ZeroLengthArrays.SByte;
						return true;
					case DObject.TypeCode.Byte:
						value = ZeroLengthArrays.Byte;
						return true;
					case DObject.TypeCode.Int16:
						value = ZeroLengthArrays.Int16;
						return true;
					case DObject.TypeCode.UInt16:
						value = ZeroLengthArrays.UInt16;
						return true;
					case DObject.TypeCode.Int32:
						value = ZeroLengthArrays.Int32;
						return true;
					case DObject.TypeCode.UInt32:
						value = ZeroLengthArrays.UInt32;
						return true;
					case DObject.TypeCode.Int64:
						value = ZeroLengthArrays.Int64;
						return true;
					case DObject.TypeCode.UInt64:
						value = ZeroLengthArrays.UInt64;
						return true;
					case DObject.TypeCode.Single:
						value = ZeroLengthArrays.Single;
						return true;
					case DObject.TypeCode.Double:
						value = ZeroLengthArrays.Double;
						return true;
					case DObject.TypeCode.Decimal:
						value = ZeroLengthArrays.Decimal;
						return true;
					case DObject.TypeCode.DateTime:
						value = ZeroLengthArrays.DateTime;
						return true;
					case DObject.TypeCode.TimeSpan:
						value = ZeroLengthArrays.TimeSpan;
						return true;
					case DObject.TypeCode.String:
						value = ZeroLengthArrays.String;
						return true;
					case DObject.TypeCode.Guid:
						value = ZeroLengthArrays.Guid;
						return true;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			if (type == DObject.TypeCode.DObject)
			{
				scope = new DeserializationScope(new DObject(), num);
				value = null;
				return false;
			}
			scope = new DeserializationScope(new List<object>(), type, num);
			value = null;
			return false;
		}

		private static void AddToDObject(DObject obj, string key, DObject.TypeCode type, object value)
		{
			switch (type)
			{
				case DObject.TypeCode.DObject:
					obj.Add(key, (DObject)value);
					break;
				case DObject.TypeCode.Boolean:
					obj.Add(key, (bool)value);
					break;
				case DObject.TypeCode.Char:
					obj.Add(key, (char)value);
					break;
				case DObject.TypeCode.SByte:
					obj.Add(key, (sbyte)value);
					break;
				case DObject.TypeCode.Byte:
					obj.Add(key, (byte)value);
					break;
				case DObject.TypeCode.Int16:
					obj.Add(key, (short)value);
					break;
				case DObject.TypeCode.UInt16:
					obj.Add(key, (ushort)value);
					break;
				case DObject.TypeCode.Int32:
					obj.Add(key, (int)value);
					break;
				case DObject.TypeCode.UInt32:
					obj.Add(key, (uint)value);
					break;
				case DObject.TypeCode.Int64:
					obj.Add(key, (long)value);
					break;
				case DObject.TypeCode.UInt64:
					obj.Add(key, (ulong)value);
					break;
				case DObject.TypeCode.Single:
					obj.Add(key, (float)value);
					break;
				case DObject.TypeCode.Double:
					obj.Add(key, (double)value);
					break;
				case DObject.TypeCode.Decimal:
					obj.Add(key, (decimal)value);
					break;
				case DObject.TypeCode.DateTime:
					obj.Add(key, (DateTime)value);
					break;
				case DObject.TypeCode.TimeSpan:
					obj.Add(key, (TimeSpan)value);
					break;
				case DObject.TypeCode.String:
					obj.Add(key, (string)value);
					break;
				case DObject.TypeCode.Guid:
					obj.Add(key, (Guid)value);
					break;
				case DObject.TypeCode.DObjectArray:
					obj.Add(key, (DObject[])value);
					break;
				case DObject.TypeCode.BooleanArray:
					obj.Add(key, (bool[])value);
					break;
				case DObject.TypeCode.CharArray:
					obj.Add(key, (char[])value);
					break;
				case DObject.TypeCode.SByteArray:
					obj.Add(key, (sbyte[])value);
					break;
				case DObject.TypeCode.ByteArray:
					obj.Add(key, (byte[])value);
					break;
				case DObject.TypeCode.Int16Array:
					obj.Add(key, (short[])value);
					break;
				case DObject.TypeCode.UInt16Array:
					obj.Add(key, (ushort[])value);
					break;
				case DObject.TypeCode.Int32Array:
					obj.Add(key, (int[])value);
					break;
				case DObject.TypeCode.UInt32Array:
					obj.Add(key, (uint[])value);
					break;
				case DObject.TypeCode.Int64Array:
					obj.Add(key, (long[])value);
					break;
				case DObject.TypeCode.UInt64Array:
					obj.Add(key, (ulong[])value);
					break;
				case DObject.TypeCode.SingleArray:
					obj.Add(key, (float[])value);
					break;
				case DObject.TypeCode.DoubleArray:
					obj.Add(key, (double[])value);
					break;
				case DObject.TypeCode.DecimalArray:
					obj.Add(key, (decimal[])value);
					break;
				case DObject.TypeCode.DateTimeArray:
					obj.Add(key, (DateTime[])value);
					break;
				case DObject.TypeCode.TimeSpanArray:
					obj.Add(key, (TimeSpan[])value);
					break;
				case DObject.TypeCode.StringArray:
					obj.Add(key, (string[])value);
					break;
				case DObject.TypeCode.GuidArray:
					obj.Add(key, (Guid[])value);
					break;
				default:
					throw new ArgumentOutOfRangeException("type", type, null);
			}
		}

		private static object ListToTypedArray(List<object> list, DObject.TypeCode elementType)
        {
            switch (elementType)
            {
                case DObject.TypeCode.DObject:
                    return list.Cast<DObject>().ToArray();
                case DObject.TypeCode.Boolean:
                    return list.Cast<bool>().ToArray();
                case DObject.TypeCode.Char:
                    return list.Cast<char>().ToArray();
                case DObject.TypeCode.SByte:
                    return list.Cast<sbyte>().ToArray();
                case DObject.TypeCode.Byte:
                    return list.Cast<byte>().ToArray();
                case DObject.TypeCode.Int16:
                    return list.Cast<short>().ToArray();
                case DObject.TypeCode.UInt16:
                    return list.Cast<ushort>().ToArray();
                case DObject.TypeCode.Int32:
                    return list.Cast<int>().ToArray();
                case DObject.TypeCode.UInt32:
                    return list.Cast<uint>().ToArray();
                case DObject.TypeCode.Int64:
                    return list.Cast<long>().ToArray();
                case DObject.TypeCode.UInt64:
                    return list.Cast<ulong>().ToArray();
                case DObject.TypeCode.Single:
                    return list.Cast<float>().ToArray();
                case DObject.TypeCode.Double:
                    return list.Cast<double>().ToArray();
                case DObject.TypeCode.Decimal:
                    return list.Cast<decimal>().ToArray();
                case DObject.TypeCode.DateTime:
                    return list.Cast<DateTime>().ToArray();
                case DObject.TypeCode.TimeSpan:
                    return list.Cast<TimeSpan>().ToArray();
                case DObject.TypeCode.String:
                    return list.Cast<string>().ToArray();
                case DObject.TypeCode.Guid:
                    return list.Cast<Guid>().ToArray();
                default:
                    throw new ArgumentOutOfRangeException("elementType", elementType, null);
            }
        }
	}
}
