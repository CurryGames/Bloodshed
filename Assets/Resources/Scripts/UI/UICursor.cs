using UnityEngine;
using System.Collections;

public class UICursor : MonoBehaviour {
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.ForceSoftware;
	public Vector2 hotSpot = Vector2.zero;


	public void Awake()
	{
		Cursor.SetCursor (cursorTexture, Vector2.zero, cursorMode);
	}

}
