using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class <c>LocalizedText</c> handles text change in case of language change.
/// This script should be attached to every <c>Text</c> element that is supposed to differ in language versions. Element with this script should also be assign Lang tag.
/// </summary>
public class LocalizedText : MonoBehaviour {

    /// <summary>
    /// Text's key by which proper version is looked up in language files.
    /// </summary>
    public string key;
    /// <summary>
    /// Text component.
    /// </summary>
    Text text;

    /// <summary>
    /// <c>LocalizedText</c> initialization.
    /// </summary>
    void Start()
    {
        text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
    /// <summary>
    /// Reloading text value depending on text file loaded in <c>LocalizationManager</c>.
    /// </summary>
    public void ReloadText()
    {
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}
