namespace Coral.Communication.EzCom
{
    public enum MethodInvokeResultCode
    {
        Success,
        ServiceNotFound,
        MethodNotFound,
        InvalidParameters,
        InfrastructureException,
        MethodException
    }
}