using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLaser : MonoBehaviour {


    // Use this for initialization
    public float DistanceToDrawLine = 10f;
    Vector3 DistanceVector;
    LineRenderer lineRenderer;
    Vector3 pos1;
    Vector3 pos2;
    public bool IsShowingLaser = false; 
    bool Started = false; //used for activating the ShowLaser coroutine only once 

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer) Debug.Log("No line renderer attached");
        DistanceVector = new Vector3(0, 0, DistanceToDrawLine);
        lineRenderer.widthMultiplier = 0.006f; //TODO change to 0
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShowingLaser && !Started)
        {
            lineRenderer.widthMultiplier = 0.006f;
            StartCoroutine(ShowLaser());
            Started = true;
        }

        if (!IsShowingLaser && Started)
        {
            StopCoroutine(ShowLaser());
            lineRenderer.widthMultiplier = 0;
            Started = false;
        }
    }

    IEnumerator ShowLaser()
    {
        while (IsShowingLaser)
        {
            pos1 = transform.position;
            pos2 = transform.position + DistanceVector;
            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);

            yield return new WaitForSeconds(0.05f);
        }
    }
}
