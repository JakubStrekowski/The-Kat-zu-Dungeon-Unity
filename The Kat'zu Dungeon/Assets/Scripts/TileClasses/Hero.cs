using The_Katzu_Dungeon.TileClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace The_Katzu_Dungeon
{
    public class Hero : Character
    {
        public Consumable[] equipment;
        public int currentCenterPositionX;
        public int currentCenterPositionY;
        public Weapon currentWeapon;
        public Armor currentArmor;
        public bool isAlife;
        public int maxHp;

        public Hero(int id, int posX, int posY,Map mp): base(id, posX,posY, mp)
        {
            representedByID = 2;
            isAlife = true;
            name = "Jan";
            maxHp = 6;
            hp = 6;
            passable = false;
            attack = 1;
            armor = 0;
            int currentCenterPositionX=posX;
            int currentCenterPositionY=posY;
            equipment = new Consumable[6];
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void Move(int direction) //0 up, 1 down, 2 right, 3 left
        {
            int targetPositionX = positionX;
            int targetPositionY = positionY;
            switch (direction)
            {
                case 0:
                    representedByID = 2;
                    targetPositionY = targetPositionY - 1;
                    Tile neighbor0;
                    neighbor0 = currentMap.GiveNeighbor(positionX, positionY, 0);
                    IfIsOther(neighbor0, targetPositionX, targetPositionY);
                    break;
                case 1:
                    representedByID = 10;
                    targetPositionY = targetPositionY + 1;
                    Tile neighbor1 = currentMap.GiveNeighbor(positionX, positionY, 1);
                    IfIsOther(neighbor1, targetPositionX, targetPositionY);
                    break;
                case 2:
                    representedByID = 9;
                    Tile neighbor2;
                    targetPositionX = targetPositionX + 1;
                    neighbor2 = currentMap.GiveNeighbor(positionX, positionY, 2);
                    IfIsOther(neighbor2, targetPositionX, targetPositionY);
                    break;
                case 3:
                    representedByID = 11;
                    Tile neighbor3;
                    targetPositionX = targetPositionX - 1 ;
                    neighbor3 = currentMap.GiveNeighbor(positionX, positionY, 3);
                    IfIsOther(neighbor3, targetPositionX, targetPositionY);
                    break;
            }
            Thread.Sleep(200);
        }

        void IfIsOther(Tile tile,int targetPositionX,int targetPositionY)
        {
            if (tile.passable)
            {
                currentMap.StepOnElement(positionX, positionY, targetPositionX, targetPositionY);
                if (isNearBorder())
                {
                    currentMap.MoveFocus(this);
                }
                positionX = targetPositionX;
                positionY = targetPositionY;

                if (tile is Stairs)
                {
                    currentMap.gameMaster.NextLevel();
                    currentMap.SendLog("You went down the stairs");
                    standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);

                    currentMap.MoveFocus(this);
                }
                if (tile is Coin)
                {
                    currentMap.GotGold();
                    standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                }
                IfIsConsumable(tile);
            }
            else
            {
                if (tile is Enemy)
                {
                    Enemy enm = (Enemy)tile;
                    enm.GetDmg(attack);
                    currentMap.SendLog("You hit " + enm.name + " for " + attack.ToString() + " damage!");
                }
            }
        }

        void IfIsConsumable(Tile tile)
        {
            if (tile is Consumable)
            {
                bool freeSpace = false;
                int i;
                for (i = 0; i < 6; i++)
                {
                    if (equipment[i] == null)
                    {
                        freeSpace = true;
                        break;
                    }
                }
                if (freeSpace)
                {
                    Consumable cons = (Consumable)tile;
                    AddItem(i, cons);
                    currentMap.SendLog("You picked up " + cons.name + "!");
                    standingOnTile = TileFactory.Get(0, positionX, positionY, currentMap);
                }
                else
                {
                    Consumable cons = (Consumable)tile;
                    currentMap.SendLog("You have no room for " + cons.name + "!");
                }
            }
        }

        public bool isNearBorder()
        {
            if (positionX - currentCenterPositionX > 3 || positionX - currentCenterPositionX < -3 || positionY - currentCenterPositionY < -1 || positionY - currentCenterPositionY > 1)
            {
                return true;
            }
            else return false;
        }

        public override void SetCurrentMap(Map crMap)
        {
            base.SetCurrentMap(crMap);
            currentMap.relativeCenterX = currentCenterPositionX;
            currentMap.relativeCenterY = currentCenterPositionY;
        }

        public override void GetDmg(int value)
        {
            hp = hp - (value - armor);

            float sliderHp = (float)hp / (float)maxHp;
            currentMap.SendUIInfo(2, (sliderHp.ToString()));
            if (hp <= 0)
            {
                isAlife = false;
                currentMap.HeroDied();
            }
            
        }
        public virtual void UseItem(int id)
        {
            id = id - 1;
            if (id<6)
            {
                if (equipment[id] != null)
                {
                    equipment[id].UseEffect(this);
                    RemoveItem(id);
                }
            }
            else
            {
                currentMap.SendLog("You have no item in this slot!");
            }
            
        }

        private void AddItem(int id,Consumable item)
        {
            equipment[id] = item;
            currentMap.RefreshItem(id,item.name);
        }

        private void RemoveItem(int itemId)
        {
            equipment[itemId] = null;
            currentMap.RefreshItem(itemId, "Empty");
        }

        public String ReturnWeaponName()
        {
            if (currentWeapon == null)
            {
                return "Dagger";
            }
            else
            {
                return currentWeapon.name;
            }
        }

        public String ReturnArmorName()
        {
            if (currentArmor == null)
            {
                return "No Armor";
            }
            else
            {
                return currentArmor.name;
            }
        }
    }
}
