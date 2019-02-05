using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>LocalizedImage</c> handles image change in case of language change.
/// This script should be attached to every <c>Image</c> element that is supposed to differ in language versions. Element with this script should also be assign Lang tag.
/// </summary>
public class LocalizedImage : MonoBehaviour {

    /// <summary>
    /// Polish version of the image to be displayed.
    /// </summary>
    public Sprite polishImage;
    /// <summary>
    /// English version of the image to be displayed.
    /// </summary>
    public Sprite englishImage;
    

	/// <summary>
    /// Load initial image.
    /// </summary>
	void Start () {
        ReloadImage();
	}
	
    /// <summary>
    /// Reload image depending on language value in <c>PlayerPrefs</c>
    /// </summary>
	public void ReloadImage () {
        if (PlayerPrefs.GetInt("lang") == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = polishImage;
            //print("Image should be polish");
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = englishImage;
            //print("Image should be english");
        }
	}
}
