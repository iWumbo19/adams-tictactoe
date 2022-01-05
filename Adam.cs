using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyTicTacToe
{
    static class Adam
    {
        private static Random rnd = new Random();

        internal static List<string> _winLines = new List<string>()
        {
            {"Yikes! Big L there Bud" },
            {"That's too bad. Maybe next time!" }
        };
        internal static List<string> _loseLines = new List<string>()
        {
            {"...I think something went wrong" },
            {"YOU'RE CHEATING!!" }
        };



        internal static string AdamWin()
        {            
            return _winLines[rnd.Next(_winLines.Count)];
        }
        internal static string AdamLose()
        {
            return _loseLines[rnd.Next(_loseLines.Count)];
        }
    }
}
