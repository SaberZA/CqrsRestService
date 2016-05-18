using CqrsRestService.CorePortable;

namespace CqrsRestService.CorePortable
{
    public class ServiceHost : IServiceHost
    {
        public string HostName { get; set; }

        public ServiceHost(string hostName)
        {
            HostName = hostName;
        }
    }
}