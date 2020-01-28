using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadRuntime.HighLevel
{
    public class DebugConsole
    {
        public static List<string> lines = new List<string>();

        public static void Write(string text)
        {
            lines.Add(text);
        }
    }
}
