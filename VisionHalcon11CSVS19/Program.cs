using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace VisionHalcon11CSVS19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World !!!");
            HOperatorSet.SetSystem("width", 512);
        }
    }
}
