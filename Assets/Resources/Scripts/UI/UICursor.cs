using UnityEngine;
using System.Collections;

public class UICursor : MonoBehaviour {
	public Texture2D cursorTexture;
	//public CursorMode cursorMode = CursorMode.ForceSoftware;
	//public Vector2 hotSpot = Vector2.zero;


    void Start()
    {
        Cursor.visible = false;
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - cursorTexture.width / 2, Event.current.mousePosition.y - cursorTexture.height / 2, cursorTexture.width, cursorTexture.height), cursorTexture);
    }

}
