﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Determines the selectedObject using raycast from wand position, forward
/// Activates the HighlightSelected script
/// </summary>
public class CastingToObject : MonoBehaviour {

    /// <summary>
    /// Parameter <c>IsCasting</c> is used for activating/deactivating detecting objects using wand
    /// </summary>
    public bool IsCasting = false;
    /// <summary>
    /// Parameter <c>selectedObject</c> is the name of detected object
    /// </summary>
    public static string selectedObject;
    /// <summary>
    /// Parameter<c>internalObject</c> is the detected object
    /// </summary>
    public string internalObject;

    RaycastHit theObject;
    HighlightSelected lastHighlighted = null; //last highlited object
    HighlightSelected Highlighted = null; //currently highlighted object
    Transform tempObject;


    /// <summary>
    /// Assigning tempObject transform
    /// </summary>
    void Start () {
        tempObject = transform; //temporary assignment
    }

    /// <summary>
    /// Update is called once per frame
    /// the selectedObject and highlighting is set here
    /// </summary>
    void Update () {

        if (IsCasting)
        {
            if (Physics.Raycast(transform.position, transform.forward, out theObject))
            {

                selectedObject = theObject.transform.gameObject.name;
                internalObject = theObject.transform.gameObject.name;

                if (tempObject.transform.gameObject.name != theObject.transform.gameObject.name)
                {

                    Highlighted = theObject.transform.gameObject.GetComponent<HighlightSelected>();
                    lastHighlighted = tempObject.gameObject.GetComponent<HighlightSelected>();

                    if (lastHighlighted)
                    {
                        lastHighlighted.rayHit = false;
                    }

                    if (Highlighted)
                    {
                        Highlighted.rayHit = true;
                    }

                    tempObject = theObject.transform;
                    tempObject.transform.gameObject.name = theObject.transform.gameObject.name;
                }


            }
        }
	}
}
