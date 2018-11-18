using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon.TileClasses
{
    class EmptyTile:Tile
    {
        public EmptyTile(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            passable = true;
        }
    }
}
