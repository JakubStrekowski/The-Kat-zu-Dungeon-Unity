using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace The_Katzu_Dungeon
{
    public class GameMaster : MonoBehaviour
    {
        GameHandler gameHandler;

        // Use this for initialization
        void Start()
        {
            GameObject.DontDestroyOnLoad(gameObject);
            gameHandler = new GameHandler(gameObject.GetComponent<DisplayScript>());
            gameHandler.CreateHero("Jacopo");
            gameHandler.GenerateRandom(1);
        }
        
    }
}