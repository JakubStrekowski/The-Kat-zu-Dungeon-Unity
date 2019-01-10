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
        if(Input.GetKeyDown(KeyCode.Alpha1)|| Input.GetKeyDown(KeyCode.Keypad1))
        {
            return "1";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            return "2";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            return "3";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            return "4";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            return "5";
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            return "6";
        }
        return null;
    }
}
