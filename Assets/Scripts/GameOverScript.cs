using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour 
{
	void OnGUI()
	{
		if (GUI.Button(new Rect((Screen.width / 2) - 40, ((Screen.height / 3 ) * 2) - 20, 80, 40), "Retry"))
			Application.LoadLevel("GameScene");
	}
}
