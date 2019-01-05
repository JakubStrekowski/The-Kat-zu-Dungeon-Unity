using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon.TileClasses
{
    public class Rat:Enemy
    {
        private int[] movementSequence;
        int currentMoveState = 0;
        public Rat(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            name = "Rat";
            movementSequence = new int[8] { 0,0,3,3,1,1,2,2 };
            hp = 1;
            attack = 1;
            representedByID = 6;
        }
        public override void MovementBehaviour()
        {
            moveDirection(movementSequence[currentMoveState]);
            currentMoveState = (currentMoveState + 1) % 8;
        }
    }
}
