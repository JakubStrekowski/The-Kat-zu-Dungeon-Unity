using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace The_Katzu_Dungeon
{
    public abstract class Tile
    {
        public int positionX;
        public int positionY;
        public bool passable;
        public int representedByID;
        public Map currentMap;
        public bool isVisible;

        public Tile(int id, int posX, int posY,Map mp)
        {
            isVisible = false;
            currentMap = mp;
            representedByID = id;
            positionX = posX;
            positionY = posY;
        }
    }
}
