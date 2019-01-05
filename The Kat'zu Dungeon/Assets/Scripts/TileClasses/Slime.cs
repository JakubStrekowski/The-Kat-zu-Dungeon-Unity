using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon.TileClasses
{
    public class Slime:Enemy
    {
        private int[] movementSequence;
        int currentMoveState = 0;
        bool waitOnce;
        public Slime(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            waitOnce = true;
            name = "Slime";
            movementSequence = new int[4] { 1, 2, 0, 3 };
            hp = 3;
            attack = 1;
            representedByID = 4;
        }
        public override void MovementBehaviour()
        {
            if (!waitOnce)
            {
                moveDirection(movementSequence[currentMoveState]);
                currentMoveState = (currentMoveState + 1) % 4;
            }
            waitOnce = !waitOnce;
        }
    }
}
