using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>QuestManager</c> manages the quest system.
/// </summary>
public class QuestManager : MonoBehaviour
{
    /// <summary>
    /// Parameter <c>currentQuest</c> holds the number of the quest currently being done.
    /// </summary>
    /// <seealso cref="quests"/> <seealso cref="Start"/>
	public int currentQuest = 0;
    /// <summary>
    /// Parameter <c>displayTime</c> is the amount of time left that the quest's text will remain on-screen. It is hard coded as 5f.
    /// </summary>
    /// <seealso cref="questLabel"/>
	float displayTime = 5f;
    /// <summary>
    /// List <c>quests</c> holds references to each quest object governing all quests in a given playthrough.
    /// </summary>
    /// <seealso cref="Start"/>
	public Quest[] quests;
    /// <summary>
    /// Game object <c>questPanel</c> holds the reference to GUI canvas that display the quest's text.
    /// </summary>
    /// <seealso cref="Update"/> <seealso cref="HideQuestPanel"/>
	public GameObject questPanel;
    /// <summary>
    /// Variable <c>questLabel</c> holds the current quest's text to be displayed by <c>questPanel</c> for the duration of <c>dsiplayTime</c>.
    /// </summary>
    /// <seealso cref="StartQuest(int)"/>
	public Text questLabel;

    /// <summary>
    /// Boolean variable <c>isQuestDisplayed</c> holds the information whether a quest's text is currently displayed on-screen.
    /// </summary>
	private bool isQuestDisplayed = false;

	/// <summary>
    /// Function <c>Start</c> initializes the class <c>questManager</c> when the scene it is present in is loaded. It assignes serial numbers to each quest on the <c>quests</c> list in the sequence they will have to be completed by the player and starts the quest with the number that is the initial value of <c>currentQuest</c> which can be assigned in the <c>QuestManager</c>-class game object's Inspector view.
    /// </summary>
	void Start ()
	{
		int i = 0;
		foreach (Quest q in quests)
		{
			q.number = i++;
		}
		StartQuest(currentQuest);
	}

    /// <summary>
    /// Function <c>Update</c> is called once per frame. If a quest text is displayed (<seealso cref="isQuestDisplayed"/>) it decrements <c>displayTime</c>. When <c>displayTime</c> reaches 0, it calls <c>HideQuestPanelFunction</c> and resets <c>displayTime</c> to 5f.
    /// </summary>
    /// <seealso cref="isQuestDisplayed"/>
    void Update ()
	{
		if (isQuestDisplayed)
		{
			displayTime -= Time.deltaTime;

			//questLabel.text = "Time " + displayTime;
			if (displayTime < 0)
			{
				HideQuestPanel();
                displayTime = 5f;
			}
		}
	}

    /// <summary>
    /// Function <c>HideQuestPanel</c> hides GUI element responsible for displaying quests' text and changes the <c>isQuestDisplayed</c> variable to reflect that.
    /// </summary>
	private void HideQuestPanel()
	{
		questPanel.SetActive(false);
		isQuestDisplayed = false;
	}

    /// <summary>
    /// Function <c>StartQuest</c> starts a quest. It also holds the repository of quest's texts to be displayed and activates the <c>questPanel</c> to display them.
    /// </summary>
    /// <seealso cref="questLabel"/> <seealso cref="Update"/>
    /// <param name="questID"> is the number of the quest on the <c>quests</c> list, which is assigned in <c>Start</c></param>
    /// <seealso cref="Start"/>
	public void StartQuest(int questID)
	{
		questPanel.SetActive(true);
		isQuestDisplayed = true;
		switch (questID)
		{
			case 0:
				questLabel.text = "Take morning medicine Antemeridil from the night stand.";
				break;
			case 1:
				questLabel.text = "Find wand.";
				break;
			case 2:
				questLabel.text = "Take morning medicine Antemeridil from the night stand.";
				break;
			case 3:
				questLabel.text = "Eat something.";
				break;
			case 4:
				questLabel.text = "Take medicine  Prandium.";
				break;
			case 5:
				questLabel.text = "Brush teeth.";
				break;
			case 6:
				questLabel.text = "Find wallet.";
				break;
			case 7:
				questLabel.text = "Leave house.";
				break;
            case 8:
                questLabel.text = "Victory!";
                break;

		}
	}

    /// <summary>
    /// Function <c>SetEndofQuestByName</c> is called when a quest has been done by the player, checks whether the quest was the one that should be done currently according to the sequence of quests on the <c>quests</c> list and, if yes, ends the quest and starts the next one in the sequence. If not, it does not end the quest and leaves it undone to prevent out-of-order quest completion.
    /// </summary>
    /// <seealso cref="StartQuest(int)"/>
    /// <param name="targetQuestName"> is the name of the done quest</param>
	public void SetEndOfQuestByName(string targetQuestName)
	{
		foreach (Quest q in quests)
		{
			if ((q.targetTagName == targetQuestName) && (currentQuest == q.number) && (!q.completed))
			{
				q.completed = true;
                StartQuest(++currentQuest);
				break;
			}               
		}
	}
}
