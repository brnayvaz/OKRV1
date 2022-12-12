using BenchmarkDotNet.Running;
using BenchmarkTestPerfom;

namespace BenchmarkTestPerform
{
    public class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<BenchMethod>();
        }


    }
}