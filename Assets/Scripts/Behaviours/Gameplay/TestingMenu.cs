using UnityEngine;
using Utils;

public class TestingMenu : MonoBehaviour
{

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			BackToMenu();
		}
	}
    public void BackToMenu()
	{
		Scenes.OpenScene(Scenes.MainMenu);
	}
}
