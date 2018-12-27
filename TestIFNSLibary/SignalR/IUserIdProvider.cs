using Microsoft.AspNet.SignalR;

namespace TestIFNSLibary.SignalR
{
    public interface IUserIdProvider
    {
        string GetUserId(IRequest request);
    }
}