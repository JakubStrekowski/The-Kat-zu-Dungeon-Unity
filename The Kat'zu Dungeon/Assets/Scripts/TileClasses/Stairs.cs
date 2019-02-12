﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon
{
    public class Stairs:Tile
    {
        public Stairs(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            representedByID = 12;
            passable = true;
        }
    }
}
