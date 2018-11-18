using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon
{
    class Zombie : Enemy
    {
        bool directionHorizontal; //does zombie move left-right or up-down
        bool moveDirectionUR; //if true, moves Up/right
        Random rnd;
        public Zombie(int id, int posX, int posY,Map mp) : base(id, posX, posY, mp)
        {
            name = "Zombie";
            rnd = new Random(posX*posY);
            hp = 3;
            attack = 1;
            
            int random = rnd.Next( 2);
            if (random == 0)
            {
                directionHorizontal = true;
            }
            else
            {
                directionHorizontal = false;
            }
        }

        public override void MovementBehaviour()
        {
            if (directionHorizontal)
            {
                if (moveDirectionUR)
                {
                    if (!moveDirection(2))
                    {
                        moveDirectionUR = !moveDirectionUR;
                    }
                }
                else
                {
                    if (!moveDirection(3))
                    {
                        moveDirectionUR = !moveDirectionUR;
                    }
                }
            }
            else
            {
                if (moveDirectionUR)
                {
                    if (!moveDirection(0))
                    {
                        moveDirectionUR = !moveDirectionUR;
                    }
                }
                else
                {
                    if(!moveDirection(1))
                    {
                        moveDirectionUR = !moveDirectionUR;
                    }
                }
            }
        }
        

    }
}
