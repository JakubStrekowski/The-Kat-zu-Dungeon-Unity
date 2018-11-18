using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Katzu_Dungeon.TileClasses
{
    class KatzuAvatar:Enemy
    {
        Enemy[] body;
        private int[] movementSequence;
        int currentMoveState;
        private bool gotBody;
        public KatzuAvatar(int id, int posX, int posY, Map mp) : base(id, posX, posY, mp)
        {
            body = new Enemy[5];
            gotBody = false;
            name = "Katzu Avatar";
            movementSequence = new int[24] { 0, 1, 2, 3,1,0,3,2,1,1,0,0,2,2,3,3,0,0,1,1,3,3,2,2 };
            hp = 3;
            attack = 2;
            currentMoveState=0;
        }
        public void GetMyBody()
        {
            body[0] = (Enemy)currentMap.tileMap[positionY][positionX - 1] ;
            body[1] = (Enemy)currentMap.tileMap[positionY][positionX + 1];
            body[2] = (Enemy)currentMap.tileMap[positionY + 1][positionX - 1] ;
            body[3] = (Enemy)currentMap.tileMap[positionY + 1][positionX];
            body[4] = (Enemy)currentMap.tileMap[positionY + 1][positionX + 1];
        }
        public override void MovementBehaviour()
        {
            if (!gotBody)
            {
                gotBody = true;
                GetMyBody();
            }

            if (movementSequence[currentMoveState] == 0)
            {
                if (currentMap.GiveNeighbor(body[0].positionX, body[0].positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[0].positionX, body[0].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(positionX, positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(positionX, positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[1].positionX, body[1].positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[1].positionX, body[1].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[0].positionX, body[0].positionY, movementSequence[currentMoveState]).passable && currentMap.GiveNeighbor(positionX, positionY, movementSequence[currentMoveState]).passable && currentMap.GiveNeighbor(body[1].positionX, body[1].positionY, movementSequence[currentMoveState]).passable)
                {
                    
                    body[0].moveDirection(movementSequence[currentMoveState]);
                    moveDirection(movementSequence[currentMoveState]);
                    body[1].moveDirection(movementSequence[currentMoveState]);
                    body[2].moveDirection(movementSequence[currentMoveState]);
                    body[3].moveDirection(movementSequence[currentMoveState]);
                    body[4].moveDirection(movementSequence[currentMoveState]);
                }
                currentMoveState = (currentMoveState + 1) % 24;
                return;
            }
            if (movementSequence[currentMoveState] == 1)
            {
                if (currentMap.GiveNeighbor(body[2].positionX, body[2].positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[2].positionX, body[2].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[3].positionX, body[3].positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[3].positionX, body[3].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[4].positionX, body[4].positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[4].positionX, body[4].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[2].positionX, body[2].positionY, movementSequence[currentMoveState]).passable && currentMap.GiveNeighbor(body[3].positionX, body[3].positionY, movementSequence[currentMoveState]).passable && currentMap.GiveNeighbor(body[4].positionX, body[4].positionY, movementSequence[currentMoveState]).passable)
                {
                    body[2].moveDirection(movementSequence[currentMoveState]);
                    body[3].moveDirection(movementSequence[currentMoveState]);
                    body[4].moveDirection(movementSequence[currentMoveState]);
                    body[0].moveDirection(movementSequence[currentMoveState]);
                    moveDirection(movementSequence[currentMoveState]);
                    body[1].moveDirection(movementSequence[currentMoveState]);

                }
                currentMoveState = (currentMoveState + 1) % 24;
                return;
            }
            if (movementSequence[currentMoveState] == 2)
            {
                if (currentMap.GiveNeighbor(body[1].positionX, body[1].positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[1].positionX, body[1].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[4].positionX, body[4].positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[4].positionX, body[4].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[1].positionX, body[1].positionY, movementSequence[currentMoveState]).passable && currentMap.GiveNeighbor(body[4].positionX, body[4].positionY, movementSequence[currentMoveState]).passable)
                {
                    body[4].moveDirection(movementSequence[currentMoveState]);
                    body[1].moveDirection(movementSequence[currentMoveState]);
                    body[3].moveDirection(movementSequence[currentMoveState]);
                    moveDirection(movementSequence[currentMoveState]);
                    body[2].moveDirection(movementSequence[currentMoveState]);
                    body[0].moveDirection(movementSequence[currentMoveState]);
                }
                currentMoveState = (currentMoveState + 1) % 24;
                return;
            }
            if (movementSequence[currentMoveState] == 3)
            {
                if(currentMap.GiveNeighbor(body[0].positionX, body[0].positionY, movementSequence[currentMoveState])is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[0].positionX, body[0].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[2].positionX, body[2].positionY, movementSequence[currentMoveState]) is Hero)
                {
                    Hero enm = (Hero)currentMap.GiveNeighbor(body[2].positionX, body[2].positionY, movementSequence[currentMoveState]);
                    enm.GetDmg(attack);
                }
                if (currentMap.GiveNeighbor(body[0].positionX, body[0].positionY, movementSequence[currentMoveState]).passable && currentMap.GiveNeighbor(body[2].positionX, body[2].positionY, movementSequence[currentMoveState]).passable)
                {
                    body[2].moveDirection(movementSequence[currentMoveState]);
                    body[0].moveDirection(movementSequence[currentMoveState]);
                    body[3].moveDirection(movementSequence[currentMoveState]);
                    moveDirection(movementSequence[currentMoveState]);
                    body[4].moveDirection(movementSequence[currentMoveState]);
                    body[1].moveDirection(movementSequence[currentMoveState]);
                }
                currentMoveState = (currentMoveState + 1) % 24;
                return;
            }
        }

            public override void Die()
        {
            foreach(Enemy bodyPart in body)
            {
                bodyPart.Die();
            }
            currentMap.HeroWon();
            base.Die();
        }
    }
    }

