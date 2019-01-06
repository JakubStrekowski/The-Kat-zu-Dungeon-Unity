using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace The_Katzu_Dungeon
{
    public class GameMaster : MonoBehaviour
    {
        public static GameMaster instance;
        public GameHandler gameHandler;
        static int dungeonLevel;

        // Use this for initialization
        void Awake() { 
            if (instance == null)instance = this;
            else if (instance != this)Destroy(gameObject);
            dungeonLevel = 1;
            GameObject.DontDestroyOnLoad(gameObject);
            gameHandler = new GameHandler(gameObject.GetComponent<DisplayScript>(), gameObject.GetComponent<GameInput>(),this);
            gameHandler.CreateHero("Jacopo");
        }
        
        public void NextLevel()
        {
            dungeonLevel++;
            SceneManager.LoadScene(0);
        }
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0)
            {
                gameObject.GetComponent<DisplayScript>().FindObjects();
                gameHandler.GenerateRandom(dungeonLevel);
            }
        }

        public void SendDebug(string s)
        {
            Debug.Log(s);
        }
    }
}