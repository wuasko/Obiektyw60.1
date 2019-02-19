using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Assets.Scripts.EyeEffectsPostprocessing;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Camera to which visual impairment scripts are attached.
    /// </summary>
    public GameObject eyeEffectCamera;
    /// <summary>
    /// Player game object.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Game object responsible for text and image localization.
    /// </summary>
    public GameObject languageManager;

    /// <summary>
    /// Canvas on which the user interface is placed.
    /// </summary>  
    public GameObject mainCanvas;
    /// <summary>
    /// <c>mainPanel</c> is the initial panel of the menu.
    /// </summary>  
    public GameObject mainPanel;
    /// <summary>
    /// <c>gameModePanel</c> is menu panel displaying game mode selection.
    /// </summary>  
    public GameObject gameModePanel;
    /// <summary>
    /// <c>gameModePanel</c> is menu panel displaying configuration of story mode.
    /// </summary>  
    public GameObject storyModePanel;
    /// <summary>
    /// <c>gameModePanel</c> is menu panel displaying configuration of designer mode.
    /// </summary>  
    public GameObject designerModePanel;
    /// <summary>
    /// <c>gameModePanel</c> is menu panel displaying settings.
    /// </summary>  
    public GameObject settingsPanel;
    /// <summary>
    /// <c>gameModePanel</c> is menu panel displaying information about the project.
    /// </summary>  
    public GameObject aboutPanel;
    /// <summary>
    /// <c>confirmPanel</c> hold panel that appears when player quits game and is asked for confirmation.
    /// </summary>
    public GameObject confirmQuitPanel;

    /// <summary>
    /// Slider controlling <c>MusicMixer</c>
    /// </summary>
    public Slider volumeSlider;

    /// <summary>
    /// Script responsible for glaucoma effect.
    /// </summary>
    private GlaucomaEffecet glaucomaScript;
    /// <summary>
    /// Script responsible for depth of field effect.
    /// </summary>
    private DepthOfField depthScript;
    /// <summary>
    /// Script responsible for yellowing of the lense effect.
    /// </summary>
    private YellowEyeEffect yellowScript;
    /// <summary>
    /// Script responsible for cataract effect.
    /// </summary>
    private CataractManager cataractScript;

    /// <summary>
    /// Audio Mixer controlling volume levels on the scene.
    /// </summary>
    public AudioMixer audioMixer;

    /// <summary>
    /// Class <c>MainMenu</c> initialization. Within it volume slider listener is assigned as well as initial values of <c>PlayerPrefs</c> attributes are set.
    /// </summary>
    void Start()
    {
        glaucomaScript = eyeEffectCamera.GetComponent<GlaucomaEffecet>();
        depthScript = eyeEffectCamera.GetComponent<DepthOfField>();
        yellowScript = eyeEffectCamera.GetComponent<YellowEyeEffect>();

      
        // Initialize language
        if (!PlayerPrefs.HasKey("lang"))
        {
            PlayerPrefs.SetInt("lang",0);
        }

        // Add volume slider listener
        volumeSlider.onValueChanged.AddListener(delegate { VolumeChanged(); });

        // Initialize volume
        if (!PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = 50;
            PlayerPrefs.SetInt("volume", 50);
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetInt("volume");
        }

        // Reset menu to proper layer configuration
        BackToMainPanel();
    }

    /// <summary>
    /// Function saving volume to <c>PlayerPrefs</c>.
    /// </summary>
    private void VolumeChanged()
    {
        PlayerPrefs.SetInt("volume", Mathf.RoundToInt(volumeSlider.value));
    }

    /// <summary>
    /// Function saving language to <c>PlayerPrefs</c>
    /// </summary>
    /// <param name="languageFile"></param>
    public void LanguageChanged(String languageFile)
    {
        if (languageFile == "localizedText_pl.json")
        {
            PlayerPrefs.SetInt("lang", 1);
        }
        else
        {
            PlayerPrefs.SetInt("lang", 0);
        }
        Debug.Log("Loading " + languageFile);
        ActivateAllPanels();
        languageManager.GetComponent<LocalizationManager>().LoadLocalizedText(languageFile);
        DeactivateAllPanels();

    }

    /// <summary>
    /// Function <c>Update</c> keeping the menu in front of player.
    /// </summary>
    void Update()
    {
        //Should we update the position of the canvas?
        mainCanvas.transform.position = eyeEffectCamera.transform.position + eyeEffectCamera.transform.forward * 1;
        mainCanvas.transform.rotation = new Quaternion(0.0f, eyeEffectCamera.transform.rotation.y, 0.0f, eyeEffectCamera.transform.rotation.w);

    }

    /// <summary>
    /// Function activating all panels for language update.
    /// </summary>
    private void ActivateAllPanels()
    {
        mainPanel.SetActive(true);
        gameModePanel.SetActive(true);
        storyModePanel.SetActive(true);
        designerModePanel.SetActive(true);
        settingsPanel.SetActive(true);
        aboutPanel.SetActive(true);
        confirmQuitPanel.SetActive(true);
        Debug.Log("Activating all panels.");
    }

    /// <summary>
    /// Function hiding all panels.
    /// </summary>
    private void DeactivateAllPanels()
    {
        mainPanel.SetActive(false);
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(true);  // Only settings should stay on
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
        Debug.Log("Deactivating panels.");
    }

    /// <summary>
    /// Function handling transition to game mode options menu.
    /// </summary>
    public void OpenStartOptionsMenu()
    {
        mainPanel.SetActive(false);
        gameModePanel.SetActive(true);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
    }

    /// <summary>
    /// Function handling transition to story menu.
    /// </summary>
    public void OpenStartStoryMenu()
    {
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(true);

        PlayerPrefs.SetInt("GameMode", 0);
    }

    /// <summary>
    /// Function handling transition to the designer menu.
    /// </summary>
    public void OpenStartDesignerMenu()
    {
        gameModePanel.SetActive(false);
        designerModePanel.SetActive(true);

        PlayerPrefs.SetInt("GameMode", 1);
    }


    /// <summary>
    /// Function that loads the <c>sceneName</c> scene.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(PlayerPrefs.GetInt("GameMode"));
    }

    /// <summary>
    /// Function handling transition to main panel.
    /// </summary>
    public void BackToMainPanel()
    {
        mainPanel.SetActive(true);
        gameModePanel.SetActive(false);
        storyModePanel.SetActive(false);
        designerModePanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(false);
        confirmQuitPanel.SetActive(false);
    }

    /// <summary>
    /// Function used by yellowing toggle button.
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleYellowing(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("yellowing", 1);
            yellowScript.enabled = true;
        }
        else
        {
            yellowScript.enabled = false;
            PlayerPrefs.SetInt("yellowing", 0);
        }
    }

    /// <summary>
    /// Function used by glaucoma toggle button.
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleGlaucoma(Toggle toggle)
    {
        if (toggle.isOn)
        {
            glaucomaScript.enabled = true;
        }
        else
        {
            glaucomaScript.enabled = false;
        }
    }

    /// <summary>
    /// Function used by yellowing toggle button.
    /// </summary>
    /// <param name="toggle"></param>
    public void ToggleDepth(Toggle toggle)
    {
        if (toggle.isOn)
        {
            depthScript.enabled = true;
        }
        else
        {
            depthScript.enabled = false;
        }
    }

    /// <summary>
    /// Function setting mixer volume to <c>sliderValue</c>.
    /// </summary>
    /// <param name="sliderValue"></param>
    public void SetLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    /// <summary>
    /// Function handling transition to <c>AboutPanel</c>.
    /// </summary>
    public void OpenPanelAbout()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    /// <summary>
    /// Function handling transition to <c>SettingsPanel</c>.
    /// </summary>
    public void OpenPanelSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        aboutPanel.SetActive(false);
    }

    /// <summary>
    /// Function handling exit button.
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Quit called!");
        Application.Quit();
    }

    /// <summary>
    /// Function handling transition to <c>ConfirmQuitPanel</c>.
    /// </summary>
    public void OpenPanelConfirmQuit()
    {
        mainPanel.SetActive(false);
        confirmQuitPanel.SetActive(true);
    }

}