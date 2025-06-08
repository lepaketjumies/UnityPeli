using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CursorChange : MonoBehaviour{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    public static bool cursorChanged = false;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            cursorChanged = true;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            cursorChanged = false;
        }
    }
}