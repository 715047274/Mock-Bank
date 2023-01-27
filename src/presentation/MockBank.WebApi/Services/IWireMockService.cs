using WireMock;
using WireMock.Org.Abstractions;
using WireMock.Util;

namespace MockBank.WebApi.Services
{
    public interface IWireMockService
    {
        void Start();

        void Stop();

        object RespondMessage(BodyData bodyData);
    }
}