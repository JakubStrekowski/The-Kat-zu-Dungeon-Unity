﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using The_Katzu_Dungeon.TileClasses;

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
        public Sprite backHero;
        public Sprite sideHero;
        public Sprite sideLeftHero;
        public Sprite stairs;

        public GameObject simpleTile;
        private InputField logInput;
        private Text heroNameTxt;
        private Text[] items;
        private Text goldAmmnt;
        private Text enemiesKilled;
        private Slider heroHpSlider;
        private GameObject camera;
        private Color dungeonColor;
        private Color wallsColor;

        private int heroVision = 6;

        private int logCounter;
        private int logNumber;

        public void FindObjects()
        {
            camera = GameObject.Find("Main Camera");
            heroNameTxt = GameObject.Find("HeroNameTxt").GetComponent<Text>();
            goldAmmnt = GameObject.Find("moneyTxt").GetComponent<Text>();
            enemiesKilled = GameObject.Find("killsTxt").GetComponent<Text>();
            heroHpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
            for(int i = 1; i < 7; i++)
            {
                items[i - 1] = GameObject.Find("Item" + i.ToString()).GetComponent<Text>();
            }
            logInput = GameObject.Find("LogInput").GetComponent<InputField>();

        }

        private void Awake()
        {
            logCounter = 0;
            logNumber = 0;
            items = new Text[6];
            FindObjects();
        }

        private void SetVisible(GameObject tile,bool toVisible)
        {
            if (toVisible)
            {
                tile.GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
            else
            {
                tile.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }

        public void DrawStory() { }
        public void DrawCredits() { }
        public void DisplayMap(Map mapObject, int centerX, int centerY) {
            FindObjects();
            float paramZ = 0;
            foreach(Tile[] row in mapObject.tileMap)
            {
                paramZ = paramZ + 0.01f;
                foreach(Tile tile in row)
                {
                    GameObject newTile = Instantiate(simpleTile, new Vector3(tile.positionX, tile.positionY,paramZ), Quaternion.identity);
                    newTile.GetComponentInChildren<SpriteRenderer>().sprite = ReturnSpriteByID(tile.representedByID);
                    if (tile.representedByID == 0|| tile.representedByID == 12) { newTile.GetComponentInChildren<SpriteRenderer>().color = dungeonColor; newTile.tag = "Floor"; }
                    if (tile.representedByID == 1) { newTile.GetComponentInChildren<SpriteRenderer>().color = wallsColor;
                        newTile.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
                    }
                    if(tile is Character || tile is Consumable||tile is Coin)
                    {
                        newTile.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
                        GameObject tileUnder=Instantiate(simpleTile, new Vector3(tile.positionX, tile.positionY, paramZ), Quaternion.identity);
                        tileUnder.GetComponentInChildren<SpriteRenderer>().sprite = ReturnSpriteByID(0);
                        tileUnder.GetComponentInChildren<SpriteRenderer>().color = dungeonColor;
                        tileUnder.tag = "Floor";
                        SetVisible(tileUnder, false);
                    }
                    if(tile is Character)
                    {
                        newTile.tag = "Character";
                    }
                    SetVisible(newTile, false);
                }
            }
            float z = camera.transform.position.z;
            camera.transform.position = new Vector3(centerX+0.5f, centerY + 0.5f, z);
        }

        public void RandomizeDungeonColor()
        {
            dungeonColor = new Color(Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f));
            wallsColor = new Color(Random.Range(0.15f, 0.4f), Random.Range(0.15f, 0.4f), Random.Range(0.15f, 0.4f));
        }

        public void RefreshFromMapAtPosition(Map mapObject, int posX, int posY)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll((new Vector3(posX,posY, 0)),Vector2.zero);
            Color floorColor=dungeonColor;
            if (hit.Length != 0)
            {
                foreach(RaycastHit2D raycastHits in hit)
                {
                    if (raycastHits.collider.gameObject.transform.parent.tag == "Floor")
                    {
                        floorColor = raycastHits.collider.gameObject.GetComponent<SpriteRenderer>().color;
                    }
                    GameObject.Destroy(raycastHits.collider.gameObject.transform.parent.gameObject);
                }
                
            }
            
            GameObject newTile = Instantiate(simpleTile, new Vector3(posX, posY, 0+(posY*0.01f)), Quaternion.identity);
            newTile.GetComponentInChildren<SpriteRenderer>().sprite = ReturnSpriteByID(mapObject.tileMap[posY][posX].representedByID);
            if (mapObject.tileMap[posY][posX].representedByID == 0) { newTile.GetComponentInChildren<SpriteRenderer>().color = dungeonColor; }
            if (mapObject.tileMap[posY][posX].representedByID == 1) { newTile.GetComponentInChildren<SpriteRenderer>().color = wallsColor; }
            if (mapObject.tileMap[posY][posX] is Character || mapObject.tileMap[posY][posX] is Consumable|| mapObject.tileMap[posY][posX] is Coin)
            {
                newTile.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
                GameObject tileUnder = Instantiate(simpleTile, new Vector3(posX, posY, 0 + (posY * 0.01f)), Quaternion.identity);
                tileUnder.GetComponentInChildren<SpriteRenderer>().sprite = ReturnSpriteByID(0);
                tileUnder.GetComponentInChildren<SpriteRenderer>().color = floorColor;
                tileUnder.tag = "Floor";
                if(!mapObject.tileMap[posY][posX].isVisible&&!mapObject.tileMap[posY][posX].wasVisited)
                SetVisible(tileUnder, false);
                
            }
            if (mapObject.tileMap[posY][posX] is Character)
            {
                newTile.tag = "Character";
            }
            SetVisible(newTile, mapObject.tileMap[posY][posX].isVisible);
        }

        public void RefreshVisibility(int heroPosX, int heroPosY, Tile[][] tileMap)
        {
            foreach(Tile[] tileSet in tileMap)
            {
                foreach(Tile tile in tileSet)
                {
                    tile.isVisible = false;
                    RaycastHit2D[] hit = Physics2D.RaycastAll((new Vector3(tile.positionX, tile.positionY, 0)), Vector2.zero);
                    foreach (RaycastHit2D ht in hit)
                    {
                        if (!tile.isVisible && tile.wasVisited)
                        {
                            ht.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color = 
                                Color.Lerp(ht.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color, Color.black, 0.15f);
                        }
                        else
                        {
                            SetVisible(ht.collider.gameObject, false);
                        }
                    }
                }
            }
            for (int i = 0; i < 360; i++)
            {
                float x = Mathf.Cos((float)i * 0.01745f); 
                float y = Mathf.Sin((float)i * 0.01745f);
                DoFov(x, y,heroPosX,heroPosY,tileMap);
            }
        }

        private void DoFov(float x, float y, int heroPosX, int heroPosY, Tile[][] tileMap)
        {
            int i;
            float ox, oy;
            ox = (float)heroPosX + 0.5f;
            oy = (float)heroPosY + 0.5f;
            for (i = 0; i < heroVision; i++)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll((new Vector3((int)ox, (int)oy, 0)), Vector2.zero);
                foreach (RaycastHit2D ht in hit)
                {
                    tileMap[(int)oy][(int)ox].isVisible = true;
                    tileMap[(int)oy][(int)ox].wasVisited = true;
                    SetVisible(ht.collider.gameObject, true);
                    if (ht.collider.gameObject.transform.parent.tag == "Floor")
                    {
                        ht.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color = dungeonColor;
                    }
                    else
                    {
                        if (tileMap[(int)oy][(int)ox] is Character || tileMap[(int)oy][(int)ox] is Consumable || tileMap[(int)oy][(int)ox] is Coin)
                        {
                            ht.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                        }
                        else
                        {
                            if (tileMap[(int)oy][(int)ox] is Wall)
                            {
                                ht.collider.gameObject.GetComponentInChildren<SpriteRenderer>().color = wallsColor;
                            }
                        }
                    }
                }
                
                if (tileMap[(int)oy][(int)ox] is Wall)
                    return;
                ox += x;
                oy += y;
            };
        }

        public void MoveFocus(int positionX, int positionY)
        {
            float z = camera.transform.position.z;
            camera.transform.position = new Vector3(positionX + 0.5f, positionY + 0.5f, z);
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
        {
            switch (which)
            {
                case 1: heroNameTxt.text = System.Environment.UserName;break;
                case 2: heroHpSlider.value = float.Parse(value);
                    break;
                case 6: goldAmmnt.text = "x " + value;break;
                case 7: enemiesKilled.text = "x " + value;break;

                default:break;
            }
        }

        public void AddLog(string log)
        {
            logCounter = (logCounter + 1);
            logNumber= (logNumber + 1) % 1000;
            if (logCounter > 7)
            {
                logCounter = 1;
                logInput.text = "";
            }
            logInput.text +=(logNumber).ToString()+". "+ log + '\n';
            logInput.MoveTextEnd(true);
        }
            private void PrintLastLog()
        {
        }


        public void RefreshItem(int idInList, string newName)
        {
            if (idInList == -1)
            {
            }
            else
            {
                items[idInList].text = (idInList + 1).ToString() + ". " + newName;
            }
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
                case 9: toReturn = sideHero;break;
                case 10: toReturn = backHero;break;
                case 11:toReturn = sideLeftHero;break;
                case 12:toReturn = stairs;break;
            }
            return toReturn;
        }

        public void AnimateTile(int animationID, int positionX, int positionY, int targetX, int targetY, Map map)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll((new Vector3(positionX, positionY, 0)), Vector2.zero);
            GameObject hitCharacter = null;
            foreach (RaycastHit2D ht in hit)
            {
                if (ht.collider.gameObject.transform.parent.tag == "Character")
                {
                    hitCharacter = ht.collider.gameObject;
                }
            }
                hitCharacter.GetComponentInChildren<SpriteRenderer>().sprite = ReturnSpriteByID(map.tileMap[targetY][targetX].representedByID);
               // hitCharacter.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                if (hit.Length != 0)
                {
                    Animator animator = hitCharacter.GetComponentInChildren<Animator>();

                    switch (animationID)
                    {
                        case 0: animator.Play("GoDown"); break;
                        case 1: animator.Play("GoUp"); break;
                        case 2: animator.Play("GoRight"); break;
                        case 3: animator.Play("GoLeft"); break;
                        default: break;
                    }
                    StartCoroutine(DestroyAfterAnimation(hitCharacter, targetX, targetY, map));

                }
            }
        

        IEnumerator DestroyAfterAnimation(GameObject gmObject,int targetX,int targetY,Map map)
        {
            yield return new WaitForSeconds(0.2f);
            Destroy(gmObject);
            RefreshFromMapAtPosition(map, targetX, targetY);
        }
    }
    }
