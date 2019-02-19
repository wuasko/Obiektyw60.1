using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>Quest</c> manages the bahaviour of each individual quest in the quest system.
/// </summary>
public class Quest : MonoBehaviour
{
    //public bool enter = false;
    //public GameObject targetCollider;
    //public bool pickUp = false;
    //public GameObject subjectCollider;

    /// <summary>
    /// Parameter <c>number</c> is the number of the quest in the quest sequence. It is assigned to the quest by <c>QuestManager</c> method <c>Start</c> when the scene is loaded.
    /// </summary>
    public int number;
    /// <summary>
    /// Variable <c>targetTagName</c> holds the string corresponding to the name of the tag corresponding to the quest. Colliders with this tag will trigger the completion of the quest.
    /// </summary>
    public string targetTagName;
    /// <summary>
    /// Boolean variable <c>completed</c> holds the information whether the quest has been completed or not.
    /// </summary>
    public bool completed = false;

    /* unimplemented
    /// <summary>
    /// List <c>highlightedGameObjects</c> holds the list of references to the objects in the scene that should be highlighted while the quest is active.
    /// </summary>
    public GameObject[] highlightedGameObjects;
    */
}
