using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using System.Threading;

namespace The_Katzu_Dungeon
{
   public class GameHandler
    {
        public int enemiesKilled;
        private int gold;
        public int floorNumber;
        Map currentMap;
        DisplayScript display;
        GameInput input;
        List<Enemy> enemiesOnMap;
        public Hero hero;
        int whatInControl = 0; //0-hero, 1-game menu, 2-death menu, 3-start menu, 4 ending
        GameMaster gameMaster;

        public GameHandler(DisplayScript display, GameInput gameInput, GameMaster gameMaster)
        {
            enemiesOnMap = new List<Enemy>();
            this.display = display;
            floorNumber = 1;
            input = gameInput;
            this.gameMaster = gameMaster;
        }

        public void SetGold(int value)
        {
            gold = value;
            display.SetStatUI(6, gold.ToString());
        }
        public void AddGold(int value)
        {
            gold += value;
            display.SetStatUI(6, gold.ToString());
        }

        public void CreateHero(string name)
        {
            hero = new Hero(2, 0, 0, null);
            enemiesKilled = 0;
            gold = 0;
            hero.SetName(name);
        }
    

        void ResolveTurn() {
            foreach(Enemy enemy in enemiesOnMap)
            {
                if (hero.isAlife)
                {
                    enemy.MovementBehaviour();
                }
                
            }
            
        }

        public void NextLevel()
        {
            gameMaster.NextLevel();
        }

        public Map GenerateRandom(int floorNumber)
        {

            Map currentMap = new Map();
            enemiesOnMap = new List<Enemy>();
            System.Random rnd = new System.Random();
            if (floorNumber == 5)
            {
                Map newMap1 = LoadMap("2.txt");
                return newMap1;
            }
            DungeonGenerator mapGenerator = new DungeonGenerator(100, 50);
            int[][] dungeon=new int[100][];
            int rowAmmount = 0;
            int columnAmmount = 0;
            switch (floorNumber)
            {
                case 1:
                    rowAmmount = 25;
                    columnAmmount = 50;
                    dungeon = mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 8);
                    break;
                case 2:
                    rowAmmount = 35;
                    columnAmmount = 70;
                    dungeon = mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 13);
                    break;
                case 3:
                    rowAmmount = 45;
                    columnAmmount = 85;
                    dungeon = mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 16);
                    break;
                case 4:
                    rowAmmount = 50;
                    columnAmmount = 100;
                    dungeon = mapGenerator.CreateDungeon(columnAmmount, rowAmmount, 20);
                    break;
            }
            
            Map newMap = new Map(dungeon, display, this,rowAmmount,columnAmmount);
            display.DrawFrame();
            currentMap = newMap;
            hero.SetCurrentMap(currentMap);
            ChangeFloorNumber(floorNumber);
            display.SetStatUI(1, hero.name);
            float sliderHp = (float)hero.hp / (float)hero.maxHp;
            display.SetStatUI(2, sliderHp.ToString());
            gameMaster.SendDebug(hero.hp + " " + hero.maxHp);
            gameMaster.SendDebug(sliderHp.ToString());
            display.SetStatUI(6, gold.ToString());
            display.SetStatUI(7, enemiesKilled.ToString());
            bool displayed = false;
            for (int i = 0; i < 6; i++)
                if (hero.equipment[i] != null)
                {
                    display.RefreshItem(i, hero.equipment[i].name);
                    displayed = true;
                    break;
                }
            if (!displayed)
            {
                display.RefreshItem(-1, "Whatever");
            }
            whatInControl = 0;
            currentMap.SetFocus();
            return newMap;
        }

    public Map LoadMap(string name="1.txt")
        {
            enemiesOnMap = new List<Enemy>();
            display.DrawFrame();
            currentMap = new Map();
            int mapRowLimit=50;
            int mapColumnLimit = 100;
            int[][] intMap = new int[mapRowLimit][];
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("maps/"+name);
            int rowCounter = 0;
            while(rowCounter!= mapRowLimit)
            if((line = file.ReadLine()) != null)
            {
                string[] strRow = line.Split(' ');
                int[] intRow = new int[mapColumnLimit];
                int counter = 0;
                foreach (string st in strRow)
                {
                    intRow[counter] = int.Parse(st);
                    counter++;
                }
                    while (counter < mapColumnLimit)
                    {
                        intRow[counter] = 0;
                        counter++;
                    }
                intMap[rowCounter] = intRow;
                rowCounter++;
            }
                else
                {
                    while (rowCounter < mapRowLimit)
                    {
                        intMap[rowCounter] = new int[mapColumnLimit];
                        int counter = 0;
                        while (counter < mapColumnLimit)
                        {
                            intMap[rowCounter][counter] = 0;
                            counter++;
                        }
                        rowCounter++;
                    }
                    
                }
            file.Close();
            Map newMap = new Map(intMap, display, this, mapRowLimit, mapColumnLimit);
            display.DrawFrame();
            currentMap = newMap;
            hero.SetCurrentMap(currentMap);
            ChangeFloorNumber(floorNumber);
            display.SetStatUI(1, hero.name);
            float sliderHp = (float)hero.hp / (float)hero.maxHp;
            display.SetStatUI(2, sliderHp.ToString());
            gameMaster.SendDebug(hero.hp + " " + hero.maxHp);
            gameMaster.SendDebug(sliderHp.ToString());
            display.SetStatUI(6, gold.ToString());
            display.SetStatUI(7, enemiesKilled.ToString());
            bool displayed = false;
            for (int i = 0; i < 6; i++)
                if (hero.equipment[i] != null)
                {
                    display.RefreshItem(i, hero.equipment[i].name);
                    displayed = true;
                    break;
                }
            if (!displayed)
            {
                display.RefreshItem(-1, "Whatever");
            }
            whatInControl = 0;
            currentMap.SetFocus();
            return newMap;
        }

        public void PlayInMap()
        {
                if (ResolveInput(input.TakeInput()))
                {
                    ResolveTurn();
                
            }
        }

        public bool ResolveInput(String inputCommand)
        {
            if (whatInControl == 0)
                switch (inputCommand) {
                
                case "ArrowUp":
                    {
                        hero.Move(1);
                    }
                    return true;
                case "ArrowDown":
                    {
                        hero.Move(0);
                    }
                    return true;
                case "ArrowRight":
                    {
                        hero.Move(2);
                    }
                    return true;
                case "ArrowLeft":
                    {
                        hero.Move(3);
                    }
                    return true;
                    case "1":
                        {
                            hero.UseItem(1);
                            return true;
                        }
                    case "2":
                        {
                            hero.UseItem(2);
                            return true;
                        }
                    case "3":
                        {
                            hero.UseItem(3);
                            return true;
                        }
                    case "4":
                        {
                            hero.UseItem(4);
                            return true;
                        }
                    case "5":
                        {
                            hero.UseItem(5);
                            return true;
                        }
                    case "6":
                        {
                            hero.UseItem(6);
                            return true;
                        }
                    case "Escape":
                case "Q":
                    {
                        whatInControl = 1;
                            display.DisplayMenu();
                    }
                    return false;
                default: return false;
            }
            if (whatInControl == 1)
            {
                switch (inputCommand)
                {
                    case "E":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    case "Escape":
                    case "C":
                        {
                            hero.currentMap.MoveFocus(hero);
                            whatInControl = 0;
                            break;
                        }
                    default:
                        break;
                }
                return false;
            }
            if (whatInControl == 2)
            {
                switch (inputCommand)
                {
                    case "S":
                        CreateHero(hero.name);
                        floorNumber = 1;
                        GenerateRandom(floorNumber);
                        return false;
                    case "E":
                        Environment.Exit(0);
                        return false;
                    default:
                        return false;
                }
            }
            if(whatInControl == 4)
            {
                switch (inputCommand)
                {
                    case "Enter":
                        Environment.Exit(0);
                        return false;
                    default:
                        return false;
                }
            }
            else return false;
        }

        private void ChangeFloorNumber(int value)
        {
            display.SetStatUI(0, floorNumber.ToString());
        }

        public void AddEnemyToList(Enemy toAdd)
        {
            enemiesOnMap.Add(toAdd);
        }

        public void RemoveEnemyFromList(Enemy toRemove)
        {
            enemiesOnMap.Remove(toRemove);
        }
        public void SetWhatInControl(int value)
        {
            whatInControl = value;
        }
    }
}
