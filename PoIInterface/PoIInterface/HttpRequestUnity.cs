using System;
using System.IO;
using System.Net;

namespace PoIInterface
{
    public class HttpRequestUnity : IHttpRequest
    {
        private const string strApplicationJSON = "application/json";
        private const string strMethodPOST = "POST";
        private const string strHttpErrorFormat =  "Server error (HTTP {0}: {1}).";

        public string GetRequest(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Accept = strApplicationJSON;
            
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new System.Exception(string.Format(
                        strHttpErrorFormat,
                        response.StatusCode,
                        response.StatusDescription));
                else
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        string result = sr.ReadToEnd();
                        return result;
                    }
                }
            }
            
            return null;
        }

        public string PostRequest(string jsonRequest, string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = strApplicationJSON;
            httpWebRequest.Accept = strApplicationJSON;
            httpWebRequest.Method = strMethodPOST;
            
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonRequest);
                streamWriter.Flush();
                streamWriter.Close();
                
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

    }
}

