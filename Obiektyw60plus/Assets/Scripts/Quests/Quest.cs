using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool enter = false;
    public GameObject enterTargetPlace;
    public bool pickUp = false;
    public GameObject pickUpTarget;
    public GameObject[] highlightedGameObjects;
    public string text;
    public bool completed = false;

    public void StartQuest()
    {

    }

    public void EndQuest()
    {
        completed = true;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
