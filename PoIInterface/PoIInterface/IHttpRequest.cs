using System;
namespace PoIInterface
{
    public interface IHttpRequest
    {
        string GetRequest(string url);
        string PostRequest(string jsonRequest, string url);
    }
}

