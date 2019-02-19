using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the object to the point, determined by WandOfMoveFurniture script
/// Moving only in Y axis, up or down
/// Requires also HighlightSelected script attached.
/// </summary>
public class MovableInYAxis : MonoBehaviour {

    /// <summary>
    /// Parameter<c>selectedObject</c> is the object currently selected
    /// </summary>
    public GameObject selectedObject;

    HighlightSelected highlightSelected;
    bool lookingAtObject = false;
    bool coroutineStarted = false;
    bool startedMoving = false;
    Rigidbody rigidbody;
    public int cnt = 0;

    GameObject wand;
    WandOfMoveFurniture wandOfMove;


/// <summary>
/// Initialization of wand, wandOfMove, highlightSelected and rigidbody variables
/// </summary>
    void Start()
    {

        wand = GameObject.Find("WandOfMoveFurniture");
        if (!wand) Debug.Log("Can't find wand of move furniture");
        wandOfMove = wand.GetComponent<WandOfMoveFurniture>();
        if (!wandOfMove) Debug.Log("Can't find script WandOfMoveFurniture");
        highlightSelected = GetComponent<HighlightSelected>();
        if (!highlightSelected) Debug.Log("no HighlightSelected script attached");
        rigidbody = GetComponent<Rigidbody>();
        if (!rigidbody) Debug.Log("no Rigidbody attached");

        wandOfMove.PointToGo = transform.position;

    }

    /// <summary>
    ///  Function <c>FixedUpdate</c> is called once per frame to calculate the physics. Coroutine for moving objects is started and stopped here
    /// </summary>
    void FixedUpdate()
    {

        if (wandOfMove.move) //must hold right index finger to start moving coroutine
        {
            if (highlightSelected.rayHit && !coroutineStarted)
            {
                selectedObject = GameObject.Find(CastingToObject.selectedObject);
                lookingAtObject = true;
                StartCoroutine(MoveObject());
                coroutineStarted = true;

            }
            else if (!highlightSelected.rayHit && coroutineStarted)
            {
                lookingAtObject = false;
                StopCoroutine(MoveObject());
                coroutineStarted = false;
            }
        }

        if (coroutineStarted && !wandOfMove.move)
        {
            StopCoroutine(MoveObject());
        }
    }

    /// <summary>
    /// Coroutine for moving the current object
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveObject()
    {

        while (wandOfMove.move)
        {
            Vector3 DirectionToGo = new Vector3(transform.position.x, wandOfMove.PointToGo.y - transform.position.y,transform.position.z);
            rigidbody.MovePosition(transform.position + DirectionToGo * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);
        }
    }
}
