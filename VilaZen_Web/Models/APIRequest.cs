using System.Net;
using static VilaZen_Utility.SD;

namespace VilaZen_Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.Get;
        public string Url { get; set; }
        public object Data { get; set; }

    }
}
