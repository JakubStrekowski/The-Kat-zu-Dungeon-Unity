using System.Collections;
using System.Collections.Generic;
using The_Katzu_Dungeon;
using UnityEngine;

public class GameInput:MonoBehaviour
{
    public GameMaster gameMaster;
    private void Awake()
    {
        gameMaster = gameObject.GetComponent<GameMaster>();
    }

    private void Update()
    {
        gameMaster.gameHandler.PlayInMap();
    }


    public string TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return "ArrowUp";
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return "ArrowDown";
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return "ArrowRight";
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return "ArrowLeft";
        }
        return null;
    }
}
