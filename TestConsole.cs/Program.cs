using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDFv3Net;

namespace TestConsole.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            //IDFv3Net.IDFFile file = new IDFv3Net.IDFFile(@"e:\pcb\hib-ip 8002 v 115b-2 comp heights1.emn");

            //var P = new IDFFile(@"e:\pcb\PanelEx.emn");
            //var B = new IDFFile(@"e:\pcb\BoardEx.emn");
            var L = new IDFFile(@"e:\pcb\LibraryEx.emp");
            var L2 = new IDFFile(@"e:\pcb\LibraryEx2.emp");
        }
    }
}
