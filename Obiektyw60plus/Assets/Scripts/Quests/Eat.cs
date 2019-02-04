using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Eat</c> is a component used by objects that should be put to mouth and disappear, mimicking eating - like apple - to trigger the completion of their quests.
/// </summary>
public class Eat : MonoBehaviour
{
    /// <summary>
    /// Method <c>OnTriggerEnter</c> checks every collision whether the other colider is OVRCameraRig's collider, which corresponds to the head of the player and moves with it. If yes, it ddstroys the object after calling the <c>PassEndQuestToQuestManager</c> method and passing the object's <c>tag</c> as <c>targetQuestName</c>. This tag corresponds to <c>Quest</c>'s <c>targetTagName</c>.
    /// </summary>
    /// <param name="collision"></param> is the collider with which the collision was detected.</param>
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "OVRCameraRig")
        {
            PassEndQuestToQuestManager(((GameObject)this.gameObject).tag);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Method <c>PassEndQuestToQuestManager</c> finds the <c>questManager</c> and class its <c>SetEndOfQuestByName</c> function passing th name of the function as <c>targetQuestName</c>, triggering the end of the quest.
    /// </summary>
    /// <param name="targetQuestName"> is the name of the quest completed, which is passed from <c>OnTriggerEnter</c> method where this function is called.</param>
    private void PassEndQuestToQuestManager(string targetQuestName)
    {
        GameObject goManager = GameObject.Find("QuestManager");
        QuestManager questManager = (QuestManager)goManager.GetComponent(typeof(QuestManager));
        questManager.SetEndOfQuestByName(targetQuestName);
    }
}
