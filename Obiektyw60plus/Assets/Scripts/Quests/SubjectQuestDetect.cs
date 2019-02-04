using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>SubjectQuestDetect</c> has functions that check the object's collisions to see if quests associated with it has been be completed.
/// </summary>
public class SubjectQuestDetect : MonoBehaviour {

    /// <summary>
    /// Boolean parameter <c>reverseTarget</c> controls whether the quest's objective is for the object to start (if FALSE) or stop (if TRUE) colliding with <c>CollisionTarget</c>
    /// </summary>
    public bool reverseTarget;
    /// <summary>
    /// Game object <c>collisionTarget</c> holds the reference to the game object with which collision is important for the quest. It can be the area to bring the object into, area to ove the object out of, or the hand's collider if the object should be grabbed or released.
    /// </summary>
    public GameObject collisionTarget;
    /// <summary>
    /// Game object <c>alternateCollisionTarget</c> holds the reference to a second game with which collision is important for the quest. It works the same as <c>collisionTarget</c>, it simply allows for two such objects. It is usually used to account for both hands of the player.
    /// </summary>
    public GameObject alternateCollisionTarget;

    /// <summary>
    /// Method <c>OnTriggerEnter</c> calls method <c>PassEndQuestToQuestManager</c> with the object's <c>tag</c> as the <c>targetQuestName</c> if the conditions are met, to trigger the completion of the quest by that name. The conditions depend on the value of <c>reverseTarget</c> and check if either <c>collisionTarget</c> or <c>alternateCollisionTarget</c> are colliding if <c>reverseTarget</c> is FALSE or both of them are not colliding if it's TRUE.
    /// </summary>
    /// <param name="collision"> is the collider of the object with which the collision was detected.</param>
    void OnTriggerEnter(Collider collision)
    {
        if (!reverseTarget)
        {
            if ((collision.gameObject.name == collisionTarget.name) || (collision.gameObject.name == alternateCollisionTarget.name))
            {
                PassEndQuestToQuestManager(((GameObject)this.gameObject).tag);
            }
        }
        else
        {
            if (!((collision.gameObject.name == collisionTarget.name) || (collision.gameObject.name == alternateCollisionTarget.name)))
            {
                PassEndQuestToQuestManager(((GameObject)this.gameObject).tag);
            }
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
