using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws line from the tip of the wand, when the wand is used
/// Uses LineRenderer component to draw the line
/// 
/// WandOfMoveFurniture controls this script
/// </summary>
public class DrawLaser : MonoBehaviour {


    /// <summary>
    /// Parameter <c>DistanceToDrawLine</c> is used to store distance to draw the rendered line
    /// </summary>
    public float DistanceToDrawLine = 10f;
    /// <summary>
    /// Parameter<c>IsShowingLaser</c> activates/deactivates drawing the laser pointer
    /// It is set in WandOfMoveFurniture script
    /// </summary>
    public bool IsShowingLaser = false;

    LineRenderer lineRenderer;
    Vector3 pos1; //position when not using
    Vector3 pos2; //position when using

    bool Started = false; //used for activating the ShowLaser coroutine only once 

    /// <summary>
    /// Initialization of LineRenderer with some initial values and start<c>pos1</c> and end<c>pos2</c> positions for rendering line 
    /// </summary>
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer) Debug.Log("No line renderer attached");
        lineRenderer.widthMultiplier = 0.006f; //TODO change to 0
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        pos1 = new Vector3(0,0,0.001f);
        pos2 = new Vector3(0, 0, DistanceToDrawLine);
    }

    /// <summary>
    /// Update is called once per frame
    /// starts/stops coroutine to draw line depending on the <c>IsShowingLaser</c> parameter and <c>Started</c> parameter
    /// </summary>
    void Update()
    {
        if (!Started)//only do this when coroutine is not active (object is not being used)
        {
            lineRenderer.SetPosition(1, pos1);
        }

        if (IsShowingLaser && !Started)
        {
            StartCoroutine(ShowLaser());
            Started = true;
        }

        if (!IsShowingLaser && Started)
        {
            StopCoroutine(ShowLaser());
            Started = false;
        }
    }

    /// <summary>
    /// Coroutine for rendering the laser
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowLaser()
    {
        while (IsShowingLaser)
        {
            lineRenderer.SetPosition(1, pos2);

            yield return new WaitForSeconds(0.05f);
        }
    }
}
