using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using The_Katzu_Dungeon.TileClasses;

namespace The_Katzu_Dungeon
{
    public class Map
    {
        DisplayScript display;
        public int relativeCenterX=0;
        public int relativeCenterY=0;
        public Tile[][] tileMap;
        private bool dontShow;
        public GameHandler gameMaster;
        int mapRowLimit;
        int mapColumnLimit;
        public Map()
        {

        }

        public Map(int[][] intMap,DisplayScript display,GameHandler gm, int rowAmmount, int ColumnAmmount)
        {
            dontShow = false;
            gameMaster = gm;
            mapRowLimit = rowAmmount;
            mapColumnLimit = ColumnAmmount;
            this.display = display;
            this.tileMap = new Tile[mapRowLimit][];
            int rowCounter = 0;
            foreach (int[] intRow in intMap)
            {
                this.tileMap[rowCounter] = new Tile[mapColumnLimit];
                int columnCounter = 0;
                foreach(int integer in intRow)
                {
                    if (integer == 2)
                    {
                        tileMap[rowCounter][columnCounter] = gameMaster.hero;
                        gameMaster.hero.positionX = columnCounter;
                        gameMaster.hero.positionY = rowCounter;
                        gameMaster.hero.currentCenterPositionX = columnCounter;
                        gameMaster.hero.currentCenterPositionY = rowCounter;
                    }
                    else this.tileMap[rowCounter][columnCounter] = TileFactory.Get(integer, columnCounter, rowCounter, this);
                    if (integer == 6 || integer==3|| integer == 8|| integer == 9||integer==11)
                    {
                        gameMaster.AddEnemyToList((Enemy)this.tileMap[rowCounter][columnCounter]);
                    }
                    
                    columnCounter++;
                }
                rowCounter++;
            }
            display.RandomizeDungeonColor();
        }

        public void SwitchElements(int sourceX,int sourceY,int targetX,int targetY)
        {
            Tile temporary = tileMap[targetY][targetX];
            tileMap[targetY][targetX] = tileMap[sourceY][sourceX];
            tileMap[sourceY][sourceX] = temporary;
            display.RefreshFromMapAtPosition(this, sourceX, sourceY);
            display.RefreshFromMapAtPosition(this, targetX, targetY);

        }

        public void StepOnElement(int sourceX,int sourceY, int targetX, int targetY)
        {
            Character chara = (Character)tileMap[sourceY][sourceX];
            Tile temporary = tileMap[targetY][targetX];
            tileMap[targetY][targetX] = tileMap[sourceY][sourceX];
            tileMap[sourceY][sourceX] = chara.standingOnTile;
            if (sourceX > targetX) { display.AnimateTile(3, sourceX, sourceY, targetX,targetY,this); }
            if (sourceX < targetX) { display.AnimateTile(2, sourceX, sourceY, targetX, targetY, this); }
            if (sourceY > targetY) { display.AnimateTile(0, sourceX, sourceY, targetX, targetY, this); }
            if (sourceY < targetY) { display.AnimateTile(1, sourceX, sourceY, targetX, targetY, this); }
            chara.standingOnTile = temporary;
            if (!dontShow)
            {
                //display.RefreshFromMapAtPosition(this, sourceX, sourceY);
               // display.RefreshFromMapAtPosition(this, targetX, targetY);
            }
        }

        public Tile GiveNeighbor(int posX, int posY, int direction) //direction going: 0-up, 1-down, 2-right, 3-left
        {
            switch (direction)
            {

                case 0:
                    return tileMap[posY-1][posX];
                case 1: return tileMap[posY+1][posX];
                case 2: return tileMap[posY][posX+1];
                case 3: return tileMap[posY][posX-1];
                default:return tileMap[posY][posX];
            }
        }

        public void MoveFocus(Hero hero)
        {
            
                relativeCenterX =hero.positionX;
            
            
                relativeCenterY= hero.positionY;
            
            hero.currentCenterPositionX=hero.positionX;
            hero.currentCenterPositionY=hero.positionY;
            display.MoveFocus(hero.positionX, hero.positionY);
        }
        
        public void SendUIInfo(int valueID, String value)
        {
            display.SetStatUI(valueID, value);
        }

        public void DestroyCharacter(int posX, int posY)
        {
            Character chara = (Character)tileMap[posY][posX];
            Tile temporary = chara.standingOnTile;
            if(chara is Enemy)
            {
                gameMaster.RemoveEnemyFromList((Enemy)chara);
                gameMaster.enemiesKilled++;
                display.SetStatUI(7, gameMaster.enemiesKilled.ToString());
            }
            tileMap[posY][posX] = temporary;
            display.RefreshFromMapAtPosition(this, posX, posY);
        }

        public void GetPotion(int posX,int posY)
        {
            Consumable consumable = (Consumable)tileMap[posX][posY];
            Tile temporary = consumable.standingOnTile;
            tileMap[posY][posX] = temporary;
            display.RefreshFromMapAtPosition(this,posX, posY);
        }
        public void SendLog(String message)
        {
            if(!dontShow)
            display.AddLog(message);
        }

        public void SetFocus()
        {
            if (gameMaster.hero.positionX < 8)
            {
                relativeCenterX = 8;
            }
            else
            {
                relativeCenterX = gameMaster.hero.positionX;
            }
            if (gameMaster.hero.positionX > mapColumnLimit - 8)
            {
                relativeCenterX = mapColumnLimit - 8;
            }

            if (gameMaster.hero.positionY < 8)
            {
                relativeCenterY= 8;
            }
            else
            {
                relativeCenterY = gameMaster.hero.positionY;
            }
            if (gameMaster.hero.positionY > mapRowLimit-8)
            {
                relativeCenterY = mapRowLimit - 8;
            }
                gameMaster.hero.currentCenterPositionX = relativeCenterX;
            gameMaster.hero.currentCenterPositionY = relativeCenterY;
            display.DisplayMap(this, relativeCenterX, relativeCenterY);
        }

        public void HeroDied()
        {
            dontShow = true;
            gameMaster.SetWhatInControl(2);
            display.DisplayDeathMenu();
        }

        public void GotGold()
        {
            Random rnd = new Random();
            int goldAmnt = rnd.Next(5, 21);
            gameMaster.AddGold(goldAmnt);
            SendLog("You found " + goldAmnt + " gold!");
        }

        public void RefreshItem(int idFromList, string newName)
        {
            display.RefreshItem(idFromList, newName);
        }

        public void HeroWon()
        {
            dontShow = true;
            gameMaster.SetWhatInControl(4);
            display.DisplayCrown();
        }
        
        public void Animate(int positionX, int positionY, int whichAnim, int targetX, int targetY)
        {
            display.AnimateTile(whichAnim, positionX, positionY,targetX,targetY,this);
        }
    }
}
