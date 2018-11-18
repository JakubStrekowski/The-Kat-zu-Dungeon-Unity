using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon
{
    class Skeleton:Enemy
    {
        private int[] movementSequence;
        int currentMoveState = 0;
        public Skeleton(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            name = "Skeleton";
            movementSequence =new int[4] { 2, 3, 0, 1 };
            hp = 2;
            attack = 1;

        }
        public override void MovementBehaviour()
        {
            moveDirection(movementSequence[currentMoveState]);
            currentMoveState = (currentMoveState + 1) % 4;
        }
        
    }
}
