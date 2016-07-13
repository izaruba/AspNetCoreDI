using System.Diagnostics;

namespace ResolveAllServices.Services
{
    public class CloudService : IService
    {
        public CloudService()
        {
            Trace.WriteLine($"CloudService created {this.GetHashCode()}");
        }

        public string Name => "cloud";
    }
}