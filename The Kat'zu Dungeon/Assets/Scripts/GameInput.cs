using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput
{

    
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
        return "asd";
    }
}
