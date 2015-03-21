using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour 
{
	void OnGUI()
	{
		if (GUI.Button(new Rect((Screen.width / 2) - 190, ((Screen.height / 3 ) * 2) - 20, 80, 40), "1"))
		{
			Config.NUMBER_OF_PLAYERS = 1;
			Application.LoadLevel("GameScene");
		}
		if (GUI.Button(new Rect((Screen.width / 2) - 90, ((Screen.height / 3 ) * 2) - 20, 80, 40), "2"))
		{
			Config.NUMBER_OF_PLAYERS = 2;
			Application.LoadLevel("GameScene");
		}
		if (GUI.Button(new Rect((Screen.width / 2) + 10, ((Screen.height / 3 ) * 2) - 20, 80, 40), "3"))
		{
			Config.NUMBER_OF_PLAYERS = 3;
			Application.LoadLevel("GameScene");
		}
		if (GUI.Button(new Rect((Screen.width / 2) + 110, ((Screen.height / 3 ) * 2) - 20, 80, 40), "4"))
		{
			Config.NUMBER_OF_PLAYERS = 4;
			Application.LoadLevel("GameScene");
		}
	}
}
