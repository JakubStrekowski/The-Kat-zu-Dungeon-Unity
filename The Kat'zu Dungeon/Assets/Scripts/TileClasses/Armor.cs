using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon
{
    public class Armor:Tile
    {
        public string name;
        int defenceValue;

        public Armor(int id, int posX, int posY,Map mp) : base(id, posX, posY,mp)
        {
            passable = true;
        }
    }
}
