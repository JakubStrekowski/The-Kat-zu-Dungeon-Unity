using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using The_Katzu_Dungeon.TileClasses;

namespace The_Katzu_Dungeon
{
   abstract public class Consumable:Tile
    {
        public Tile standingOnTile;
        public string name;
        public abstract void UseEffect(Character character);

        public Consumable(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            standingOnTile = TileFactory.Get(0, posX, posY, mp);
            passable = true;
        }
    }
}
