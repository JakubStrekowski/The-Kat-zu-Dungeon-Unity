using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace The_Katzu_Dungeon
{
    public class GameMaster : MonoBehaviour
    {
        public GameHandler gameHandler;

        // Use this for initialization
        void Start()
        {
            GameObject.DontDestroyOnLoad(gameObject);
            gameHandler = new GameHandler(gameObject.GetComponent<DisplayScript>(), gameObject.GetComponent<GameInput>());
            gameHandler.CreateHero("Jacopo");
            gameHandler.GenerateRandom(1);
        }
        
    }
}