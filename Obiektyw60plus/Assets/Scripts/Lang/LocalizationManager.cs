using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Class <c>LocalizationManager</c> handles language file loading and updating all across localization dependent elements.
/// </summary>
public class LocalizationManager : MonoBehaviour {
    
    /// <summary>
    /// Instance of the class used in enforcing singleton pattern.
    /// </summary>
    public static LocalizationManager instance;
    /// <summary>
    /// String storing result value of localized text.
    /// </summary>
    string result = "missing";
    /// <summary>
    /// Dictionary used to store language file keys and values.
    /// </summary>
    private Dictionary<string, string> localizedText;
    /// <summary>
    /// Initialization and enforcing the singleton pattern
    /// </summary>
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        // Initial language
        if (PlayerPrefs.GetInt("lang") == 0)
        {

            LoadLocalizedText("localizedText_en.json");
        }
        else
        {
            LoadLocalizedText("localizedText_pl.json");
        }
        //LoadLocalizedText("localizedText_en.json");
    }
    /// <summary>
    /// Function loading and reading json language file and storing it in <c>localizedText</c> dictionary.
    /// </summary>
    /// <param name="fileName"></param>
    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries.");
        }
        else
        {
            Debug.LogError("Cannot find file");
        }
        ReloadScene();
    }
    /// <summary>
    /// Function looking up text value in dictionary by the <c>key</c>
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Corresponding text value.</returns>
    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }
    /// <summary>
    /// Function responsible for updating all localized components on current scene.
    /// </summary>
    private void ReloadScene()
    {
        Debug.Log("Reloading scene");
        //Application.LoadLevel(Application.loadedLevel);
        GameObject[] buttonLabels;
        buttonLabels = GameObject.FindGameObjectsWithTag("Lang");
        foreach (GameObject button in buttonLabels)
        {
            try{
                button.GetComponent<LocalizedText>().ReloadText();
            }
            catch (System.Exception e1){
                try
                {
                    button.GetComponent<LocalizedImage>().ReloadImage();
                    print("Reloading image");
                }
                catch (System.Exception e)
                {
                    print("Localization error: " + button.transform.name);
                }
            }
        }
    }
}
