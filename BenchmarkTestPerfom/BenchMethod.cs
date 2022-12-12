using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkTestPerfom
{
    public class BenchMethod
    {
        [Benchmark]
        public void ForEachUnParalled()
        {
            List<int> listem = new List<int> { 1, 2, 3 };

            foreach (int i in listem)
            {

                switch (i)
                {
                    case 1:
                        Service1();
                        break;
                    case 2:
                        Service2();
                        break;
                    case 3:
                        Service3();
                        break;
                    default:
                        // code block
                        break;
                }

            }
        }

        [Benchmark]
        public void ForEachParell()
        {
            List<int> listem = new List<int> { 1, 2, 3 };

            Parallel.ForEach(listem, k => {

                switch (k)
                {
                    case 1:
                        Service1();
                        break;
                    case 2:
                        Service2();
                        break;
                    case 3:
                        Service3();
                        break;
                    default:
                        // code block
                        break;
                }

            });
        }

        private void Service1()
        {
            Thread.Sleep(1000);
        }

        private void Service2()
        {
            Thread.Sleep(2000);
        }

        private void Service3()
        {
            Thread.Sleep(3000);
        }



    }
}
