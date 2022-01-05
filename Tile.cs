using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassyTicTacToe
{
    internal class Tile
    {
        internal TileType TileType;
        internal Tile(Tile tile)
        {
            TileType = tile.TileType;
        }
        internal Tile(TileType type)
        {
            TileType = type;
        }
    }

    enum TileType
    {
        X,
        O
    }
}
