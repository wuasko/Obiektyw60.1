using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
/// <summary>
/// Checking collision with hand colliders
/// Activates the object's HighlightCollided script
/// </summary>
public class CheckHandCollision : MonoBehaviour {


    DistanceGrabbable distanceGrabbable;
    HighlightCollided highlightCollided;
    GameObject HandRight;
    GameObject HandLeft;

    bool IsSet = false;

    /// <summary>
    /// Initialization, assigning HandRight, HandLeft,  
    /// highlightCollided, distanceGrabbble variables
    /// </summary>
    void Start () {
        HandRight = GameObject.Find("DistanceHandRight");
        if (!HandRight) Debug.Log("Can't find DistanceHandRight");
        HandLeft = GameObject.Find("DistanceHandLeft");
        if (!HandLeft) Debug.Log("Can't find DistanceHandLeft");
        highlightCollided = GetComponent<HighlightCollided>();
        if (!highlightCollided) Debug.Log("Script HighlightCollided not attached");
        distanceGrabbable = GetComponent<DistanceGrabbable>();
        if (!distanceGrabbable) Debug.Log("Script DistanceGrabbable not attached");
    }
    /// <summary>
    ///  Update is called once per frame
    ///  highlightCollided.rayHit is set here when object is grabbed, IsSet is used to set the value to true only once
    /// </summary>

    void Update () {
        if (distanceGrabbable.isGrabbed && !IsSet)
        {
            highlightCollided.rayHit = true;
            IsSet = true;
        }
        if(!distanceGrabbable.isGrabbed && IsSet)
        {
            highlightCollided.rayHit = false;
            IsSet = false;
        }
	}
    /// <summary>
    ///Setting Highlighting to true when objects enter any of the triggers
    /// </summary>
    /// <param name="col"> Collider entering trigger </param>
    void OnTriggerEnter(Collider col)
    {
        if (!IsSet)
        {
            if (col.gameObject.name == "Cone")
            {
                highlightCollided.rayHit = true;
            }

            if (col.gameObject.name == "GrabVolumeBig")
            {
                highlightCollided.rayHit = true;
            }
            if (col.gameObject.name == "GrabVolumeSmall")
            {
                highlightCollided.rayHit = true;
            }
        }
    }
    /// <summary>
    /// Setting Highlighting to false when objects exit any of the triggers
    /// </summary>
    /// <param name="col"> Collider exiting trigger </param>
    void OnTriggerExit(Collider col)
    {
        if (!IsSet)
        {
            if (col.gameObject.name == "Cone")
            {
                highlightCollided.rayHit = false;
            }

            if (col.gameObject.name == "GrabVolumeBig")
            {
                highlightCollided.rayHit = false;
            }
            if (col.gameObject.name == "GrabVolumeSmall")
            {
                highlightCollided.rayHit = false;
            }
        }
    }

}
