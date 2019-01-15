using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int currentQuest = 0;
    public Quest[] quests;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetEndOfQuestByName(string targetQuestName)
    {
        foreach (Quest q in quests)
        {
            if ((q.targetTagName == targetQuestName) && (!q.completed))
            {
                q.completed = true;
                currentQuest++;
                break;
            }               
        }
    }
}
