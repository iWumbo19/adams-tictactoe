using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyTicTacToe
{
    internal struct Square
    {
        internal Tile Tile;

        internal Square(Tile tile)
        {
            Tile = new Tile(tile);
        }
    }
}
