using System;

namespace PoIInterface
{
    public class HttpRequestMock : IHttpRequest
    {
        #region IHttpRequest implementation

        public string GetRequest(string url)
        {
            throw new NotImplementedException();
        }
        public string PostRequest(string jsonRequest, string url)
        {
            throw new NotImplementedException();
        }

        #endregion
       
    }
}

