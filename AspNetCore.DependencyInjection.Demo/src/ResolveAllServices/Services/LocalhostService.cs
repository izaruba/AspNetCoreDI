using System.Diagnostics;

namespace ResolveAllServices.Services
{
    public class LocalhostService : IService
    {
        public LocalhostService()
        {
            Trace.WriteLine($"LocalhostService created {this.GetHashCode()}");
        }

        public string Name => "local";
    }
}