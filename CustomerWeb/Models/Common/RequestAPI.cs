using CustomerWeb.Models.Enumerable;

namespace CustomerWeb.Models.Common
{
    public class RequestAPI
    {
        public RequestMethodTypeEnum MethodType { get; set; }
        public string Route { get; set; }
        public string ContentType { get; set; }
        public object Body { get; set; } 
    }
}
