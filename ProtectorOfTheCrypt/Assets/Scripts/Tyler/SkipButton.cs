using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
	public Button yourButton;

	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		StoryMode storyMode = GameManager.instance.GameMode as StoryMode;

		storyMode.DialogueController.EndText(); // Closes Dialogue Box and resumes game.
	}
}
