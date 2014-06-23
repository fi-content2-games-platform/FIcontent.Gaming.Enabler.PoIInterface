using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoIInterface.PoI;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PoIInfo p = new PoIInfo();
            p.Name = "asdf";
            p.Id = Guid.NewGuid().ToString();
            p.Location = new Location(1f, 2.534234234f);

            Console.WriteLine(p);
            Console.WriteLine(p.Location);
            
            Console.ReadKey();
        }
    }
}
