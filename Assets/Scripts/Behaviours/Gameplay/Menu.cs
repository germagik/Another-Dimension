using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Menu : MonoBehaviour
{
	public GameObject mainPanel;
	public GameObject startPanel;
	public GameObject tutorialPanel;
	public GameObject creditsPanel;

    void Start()
    {
		ShowMain();
    }

	public void StartLevel()
	{
		Scenes.OpenScene(Scenes.Level1);
	}

	public void StartTestLevel()
	{
		Scenes.OpenScene(Scenes.TestLevel);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void ShowMain()
	{
		mainPanel.SetActive(true);
		startPanel.SetActive(false);
		tutorialPanel.SetActive(false);
		creditsPanel.SetActive(false);
	}

	public void ShowStart()
	{
		mainPanel.SetActive(false);
		startPanel.SetActive(true);
		tutorialPanel.SetActive(false);
		creditsPanel.SetActive(false);
	}

	public void ShowTutorial()
	{
		mainPanel.SetActive(false);
		startPanel.SetActive(false);
		tutorialPanel.SetActive(true);
		creditsPanel.SetActive(false);
	}
	public void ShowCredits()
	{
		mainPanel.SetActive(false);
		startPanel.SetActive(false);
		tutorialPanel.SetActive(false);
		creditsPanel.SetActive(true);
	}
}
