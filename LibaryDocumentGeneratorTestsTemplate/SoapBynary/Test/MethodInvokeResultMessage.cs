using System;
using System.IO;
using Coral.Polyps;

namespace Coral.Communication.EzCom.NamedPipe
{
	public sealed class MethodInvokeResultMessage
	{
		public MethodInvokeResultCode Code;

		public object ReturnValue;

		public DObject.TypeCode ReturnValueType;

		public bool IsVoid;

		public string ErrorMessage;

		public void Write(BinaryWriter writer)
		{
			writer.Write((int)Code);
			if (Code == MethodInvokeResultCode.Success)
			{
				if (IsVoid)
				{
					writer.Write((byte)0);
					return;
				}
				writer.Write((byte)1);
				SerializationHelper.Write(writer, ReturnValueType, ReturnValue);
			}
			else if (string.IsNullOrWhiteSpace(ErrorMessage))
			{
				writer.Write((byte)0);
			}
			else
			{
				writer.Write((byte)1);
				writer.Write(ErrorMessage);
			}
		}

		public static MethodInvokeResultMessage Read(BinaryReader reader)
		{
			MethodInvokeResultMessage methodInvokeResultMessage = new MethodInvokeResultMessage
			{
				Code = (MethodInvokeResultCode)reader.ReadInt32()
			};
			if (methodInvokeResultMessage.Code == MethodInvokeResultCode.Success)
			{
				if (reader.ReadByte() == 0)
				{
					methodInvokeResultMessage.IsVoid = true;
				}
				else
				{
					SerializationHelper.Read(reader, out methodInvokeResultMessage.ReturnValueType, out methodInvokeResultMessage.ReturnValue);
				}
			}
			else if (reader.ReadByte() != 0)
			{
				methodInvokeResultMessage.ErrorMessage = reader.ReadString();
			}
			return methodInvokeResultMessage;
		}

		public MethodInvokeResult ToResult()
		{
			if (Code == MethodInvokeResultCode.Success)
			{
				if (!IsVoid)
				{
					return MethodInvokeResult.SuccessValue(ReturnValue, ReturnValueType);
				}
				return MethodInvokeResult.SuccessVoid();
			}
			return MethodInvokeResult.Error(Code, ErrorMessage);
		}

		public static MethodInvokeResultMessage FromResult(MethodInvokeResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			return new MethodInvokeResultMessage
			{
				Code = result.Code,
				IsVoid = result.IsVoid,
				ErrorMessage = result.ErrorMessage,
				ReturnValueType = result.ReturnValueType,
				ReturnValue = result.ReturnValue
			};
		}
	}
}
