using System;
using System.Collections.Generic;
using Coral.Polyps;

namespace Coral.Communication.EzCom
{
	public sealed class MethodInvokeResult
	{
		public readonly MethodInvokeResultCode Code;

		public readonly object ReturnValue;

		public readonly DObject.TypeCode ReturnValueType;

		public readonly bool IsVoid;

		public readonly string ErrorMessage;

		private MethodInvokeResult(MethodInvokeResultCode code, object returnValue, DObject.TypeCode returnValueType, bool isVoid, string errorMessage)
		{
			Code = code;
			ReturnValue = returnValue;
			ReturnValueType = returnValueType;
			IsVoid = isVoid;
			ErrorMessage = errorMessage;
		}

		public static MethodInvokeResult SuccessValue(object returnValue, DObject.TypeCode returnValueType)
		{
			return new MethodInvokeResult(MethodInvokeResultCode.Success, returnValue, returnValueType, isVoid: false, null);
		}

		public static MethodInvokeResult SuccessVoid()
		{
			return new MethodInvokeResult(MethodInvokeResultCode.Success, null, (DObject.TypeCode)0, isVoid: true, null);
		}

		public static MethodInvokeResult Error(MethodInvokeResultCode code, string errorMessage = null)
		{
			if (code == MethodInvokeResultCode.Success)
			{
				throw new ArgumentOutOfRangeException("code");
			}
			return new MethodInvokeResult(code, null, (DObject.TypeCode)0, isVoid: false, errorMessage);
		}

		public Exception ToException()
        {
            switch (Code)
            {
                case MethodInvokeResultCode.Success:
                    return null;
                case MethodInvokeResultCode.ServiceNotFound:
                    return new KeyNotFoundException(string.IsNullOrWhiteSpace(ErrorMessage)
                        ? ("Вызываемый сервис не найден" + ".")
                        : ("Вызываемый сервис не найден" + ": " + ErrorMessage + "."));
                case MethodInvokeResultCode.MethodNotFound:
                    return new KeyNotFoundException(string.IsNullOrWhiteSpace(ErrorMessage)
                        ? ("Вызываемый метод не найден" + ".")
                        : ("Вызываемый метод не найден" + ": " + ErrorMessage + "."));
                case MethodInvokeResultCode.InvalidParameters:
                    return new InvalidOperationException(string.IsNullOrWhiteSpace(ErrorMessage)
                        ? ("Некорректные параметры вызова метода" + ".")
                        : ("Некорректные параметры вызова метода" + ": " + ErrorMessage + "."));
                case MethodInvokeResultCode.InfrastructureException:
                    return new InvalidOperationException(string.IsNullOrWhiteSpace(ErrorMessage)
                        ? ("Инфраструктурная ошибка при выполнении удаленного вызова метода" + ".")
                        : ("Инфраструктурная ошибка при выполнении удаленного вызова метода" + ": " + ErrorMessage +
                           "."));
                case MethodInvokeResultCode.MethodException:
                    return new InvalidOperationException(string.IsNullOrWhiteSpace(ErrorMessage)
                        ? ("Ошибка в вызываемом удаленном методе" + ".")
                        : ("Ошибка в вызываемом удаленном методе" + ": " + ErrorMessage + "."));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
	}
}
