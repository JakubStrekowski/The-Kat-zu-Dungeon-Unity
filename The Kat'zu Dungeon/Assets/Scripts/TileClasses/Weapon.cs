using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon
{
    public class Weapon:Tile
    {
        public string name;
        int attackValue;

        public Weapon(int id, int posX, int posY,Map mp) : base(id, posX, posY, mp)
        {
            passable = true;
        }
    }
}
