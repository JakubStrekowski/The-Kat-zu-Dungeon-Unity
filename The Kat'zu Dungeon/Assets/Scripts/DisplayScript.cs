using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace The_Katzu_Dungeon
{
    public class DisplayScript:MonoBehaviour
    {
        public Sprite basicFloor;
        public Sprite basicWall;
        public Sprite hero;
        public Sprite zombie;
        public Sprite slime;
        public Sprite skelton;
        public Sprite rat;
        public Sprite coin;
        public Sprite hpPotion;

        public GameObject camera;
        public GameObject simpleTile;

        private Color dungeonColor;
        private Color wallsColor;
        public void DrawStory() { }
        public void DrawCredits() { }
        public void DisplayMap(Map mapObject, int centerX, int centerY) {
            float paramZ = 0;
            foreach(Tile[] row in mapObject.tileMap)
            {
                paramZ = paramZ + 0.01f;
                foreach(Tile tile in row)
                {
                    GameObject newTile = Instantiate(simpleTile, new Vector3(tile.positionX, tile.positionY,paramZ), Quaternion.identity);
                    newTile.GetComponent<SpriteRenderer>().sprite = ReturnSpriteByID(tile.representedByID);
                    if (tile.representedByID == 0) { newTile.GetComponent<SpriteRenderer>().color = dungeonColor; }
                    if (tile.representedByID == 1) { newTile.GetComponent<SpriteRenderer>().color = wallsColor; }
                }
            }
            float z = camera.transform.position.z;
            camera.transform.position = new Vector3(centerX, centerY, z);
        }

        public void RandomizeDungeonColor()
        {
            dungeonColor = new Color(Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f));
            wallsColor = new Color(dungeonColor.r - 0.2f, dungeonColor.g - 0.2f, dungeonColor.b - 0.2f);
        }

        public void RefreshFromMapAtPosition(Map mapObject, int posX, int posY)
        {
        }
        public void DisplayMenu()
        {
        }
        public void DisplayDeathMenu()
        {
        }
        public void DisplayCrown()
        {
        }
        private void ResetLogVariables()
        {
        }
        private void PrintTile(int charID, int color)
        {

        }
        public void DrawFrame() //max column length = 119
        {
        }
        private void PrintNumberOfTimes(int number, int character)
        {
        }
        private void CleanMap()
        {
        }
        public void SetStatUI(int which, string value)
        { }
        public void AddLog(string log)
        {
        }
            private void PrintLastLog()
        {
        }


        public void RefreshItem(int idInList, string newName)
        {
        }

        public Sprite ReturnSpriteByID(int id)
        {
            Sprite toReturn = hero;
            switch (id)
            {
                case 0: toReturn = basicFloor; break;
                case 1: toReturn = basicWall; break;
                case 2: toReturn = hero; break;
                case 3: toReturn = zombie; break;
                case 4: toReturn = slime; break;
                case 5: toReturn = skelton; break;
                case 6: toReturn = rat; break;
                case 7: toReturn = coin; break;
                case 8: toReturn = hpPotion; break;
            }
            return toReturn;
        }


        }
    }