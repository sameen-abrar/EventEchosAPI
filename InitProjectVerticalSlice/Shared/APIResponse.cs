using System.Net;

namespace EventEchosAPI.Shared
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessage { get; set; }
        public object Result { get; set; }
        public APIResponse()
        {
            ErrorMessage = new List<string>();
        }
    }
}
