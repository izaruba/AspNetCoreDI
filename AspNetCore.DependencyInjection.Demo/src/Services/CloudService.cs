namespace Services
{
    public class CloudService : IService
    {
        public override string ToString()
        {
            return $"{this.GetType().Name} #{this.GetHashCode()}";
        }
    }
}