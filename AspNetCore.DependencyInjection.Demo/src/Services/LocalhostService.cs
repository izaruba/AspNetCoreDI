namespace Services
{
    public class LocalhostService : IService
    {
        public override string ToString()
        {
            return $"{this.GetType().Name} #{this.GetHashCode()}";
        }
    }
}