using System.Threading.Tasks;

namespace jane
{
    class Program
    {
        public static Task Main(string[] args)
            => Startup.StartAsync(args);
    }
}