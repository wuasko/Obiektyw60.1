using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Highlight objects when collision occurs
/// CheckHandCollision steers this script
/// </summary>

public class HighlightCollided : MonoBehaviour {

    /// <summary>
    /// Parameter<c>rayHit</c> activates/deactivates highlighting of an object
    /// </summary>
    public bool rayHit;
    /// <summary>
    /// Parameter<c>selectedObject</c> is the currently selected object
    /// </summary>
    public GameObject selectedObject;

    private int redCol;
    private int greenCol;
    private int blueCol;
    private int alpha = 80;
    private bool lookingAtObject = false;
    private bool flashingIn = true;
    private bool startedFlashing = false;
    private bool coroutineStarted = false;
    private Color32 initialColor;
    private Color32[] initialColors;
    private Color32 newColor;


    /// <summary>
    /// Initialization of colors
    /// </summary>
    private void Start()
    {
        initialColors = new Color32[transform.childCount];
    }

    /// <summary>
    /// Updates objects colors every frame
    /// </summary>
    void Update()
    {

        changeColor();

        if (rayHit && !coroutineStarted)
        {
            //selectedObject = GameObject.Find(CastingToObject.selectedObject);
            //if (!selectedObject)
            //{
            //    Debug.Log("Can't find selectedObject from CastingToObject, setting it to this gameObject");
            //    selectedObject = gameObject;
            //}
            selectedObject = gameObject;
            lookingAtObject = true;

            if (!startedFlashing)
            {
                startedFlashing = true;
                initialColor = GetComponent<Renderer>().material.color;
                getChildColor(); //save initial colors
                StartCoroutine(FlashObject());
                coroutineStarted = true;
            }
        }
        else if (!rayHit && coroutineStarted)
        {
            lookingAtObject = false;
            startedFlashing = false;
            StopCoroutine(FlashObject());
            coroutineStarted = false;
            selectedObject.transform.gameObject.GetComponent<Renderer>().material.color = initialColor;
            changeChildColor(initialColors);
        }


    }

    /// <summary>
    /// Getting color of children objects
    /// </summary>
    private void getChildColor()
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            initialColors[i] = child.GetComponent<Renderer>().material.color;
            i++;
        }
    }
    /// <summary>
    /// Changing color of children objects according to the <c>colorToSet</c> array
    /// </summary>
    /// <param name="colorToSet"> Color to set </param>
    private void changeChildColor(Color32[] colorToSet)
    {
        int i = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material.color = colorToSet[i];
            i++;
        }
    }
    /// <summary>
    ///  Returns an array with color of children objects to <c>colorToSet</c>
    /// </summary>
    /// <param name="colorToSet"> Color to set </param>
    private Color32[] setColors(Color32 colorToSet)
    {
        Color32[] newColors = new Color32[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            newColors[i] = colorToSet;
        }
        return newColors;
    }

    /// <summary>
    /// Changes color when <c>lookingAtObject</c> is true
    /// </summary>
    private void changeColor()
    {
        if (lookingAtObject)
        {
            newColor = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255); //colored mode
            selectedObject.transform.gameObject.GetComponent<Renderer>().material.color = newColor;
            //selectedObject.transform.gameObject.GetComponent<Renderer>().material.color = new Color32(initialColor.r, initialColor.g, initialColor.b, (byte)alpha); //transparent mode
            Color32[] newColors = new Color32[transform.childCount];
            newColors = setColors(newColor);
            changeChildColor(newColors);

        }
    }

    /// <summary>
    /// Coroutine for highlighting the object, by changing the RGB components of color
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashObject()
    {

        while (lookingAtObject)
        {

            yield return new WaitForSeconds(0.05f);
            //colored mode

            if (flashingIn)
            {
                if (greenCol <= 30)
                {
                    flashingIn = false;
                }
                else
                {
                    redCol -= 2;
                    greenCol -= 10;
                }

            }

            if (!flashingIn)
            {
                if (greenCol >= 250)
                {
                    flashingIn = true;
                }
                else
                {
                    redCol += 2;
                    greenCol += 10;

                }
            }

            //transparent mode

            //if (flashingIn)
            //{
            //    if (alpha <= 80)
            //    {
            //        flashingIn = false;
            //    }
            //    else
            //    {
            //        alpha -= 5;
            //    }

            //}

            //if (!flashingIn)
            //{
            //    if (alpha >= 160)
            //    {
            //        flashingIn = true;
            //    }
            //    else
            //    {
            //        alpha += 5;

            //    }
            ////}
        }
    }


}
