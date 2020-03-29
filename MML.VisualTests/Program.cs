using osu.Framework;
using osu.Framework.Platform;

namespace MML.VisualTests
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using GameHost host = Host.GetSuitableHost(@"visual-tests");
            
            host.Run(new VisualTestGame());
        }
    }
}