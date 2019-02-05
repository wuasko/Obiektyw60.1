using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Assets.Scripts.EyeEffectsPostprocessing;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ArchitectMenu : MonoBehaviour
{
    /// <summary>
    /// Toggle button responsible for wheelchair visibility.
    /// </summary>
    public Toggle wheelchairToggle;
    /// <summary>
    /// Wheelchair mesh that is toggle active by <c>wheelchairToggle</c>.
    /// </summary>
    public GameObject wheelchairMesh;
    /// <summary>
    /// Player game object.
    /// </summary>
    public GameObject player;
    /// <summary>
    /// Oculus player controller game object.
    /// </summary>
    public GameObject OVRPlayerController;

    /// <summary>
    /// Camera to which visual impairment scripts are attached.
    /// </summary>
    public GameObject eyesCamera;
    /// <summary>
    /// Canvas on which the architect user interface is placed.
    /// </summary>  
    public GameObject architectCanvas;
    /// <summary>
    /// <c>architectPanel</c> is constantly visible in the designer mode, bearing tools for architect to use.
    /// </summary>
    public GameObject architectPanel;
    /// <summary>
    /// <c>optionsPanel</c> appearing when options are toggled.
    /// </summary>    
    public GameObject optionsPanel;
    /// <summary>
    /// <c>confirmPanel</c> hold panel that appears when player quits game and is asked for confirmation.
    /// </summary>
    public GameObject confirmPanel;

    /// <summary>
    /// Slider controlling <c>MusicMixer</c>
    /// </summary>
    public Slider volumeSlider;
    /// <summary>
    /// Toggling slider managing <c>YellowEyeEffect</c> script.
    /// </summary>
    public Slider sliderYellow;
    /// <summary>
    /// Toggling slider managing <c>DepthOfField</c> script.
    /// </summary>
    public Slider sliderDepth;
    /// <summary>
    /// Toggling slider managing <c>CataractManager</c> script.
    /// </summary>
    public Slider sliderCataract;
    /// <summary>
    /// Toggling slider managing <c>GlaucomaEffect</c> script.
    /// </summary>
    public Slider sliderGlaucoma;

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
    public AudioMixer audio;

    /// <summary>
    /// Variable controlling state of yellowing effect and its toggle.
    /// </summary>
    private bool yellow;
    /// <summary>
    /// Variable controlling state of depth of field effect and its toggle.
    /// </summary>
    private bool depth;
    /// <summary>
    /// Variable controlling state of glaucoma effect and its toggle.
    /// </summary>
    private bool glaucoma;
    /// <summary>
    /// Variable controlling state of cataract effect and its toggle.
    /// </summary>
    private bool cataract;


    /// <summary>
    /// Class <c>ArchitectMenu</c> initialization. Within it initial toggle values are assigned, visual impairment scripts are looked up and slider toggle listeners are added.
    /// </summary>
    void Start()
    {
        yellow = false;
        depth = false;
        glaucoma = false;
        cataract = false;
        glaucomaScript = eyesCamera.GetComponent<GlaucomaEffecet>();
        depthScript = eyesCamera.GetComponent<DepthOfField>();
        yellowScript = eyesCamera.GetComponent<YellowEyeEffect>();
        cataractScript = eyesCamera.GetComponent<CataractManager>();
        architectPanel.SetActive(true);
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(false);

        cataractScript.enabled = true;
        wheelchairToggle.onValueChanged.AddListener(delegate {
            wheelchairToggleChanged(wheelchairToggle);
        });
        sliderYellow.onValueChanged.AddListener(delegate {
            sliderYellowToggle(sliderYellow);
        });
        sliderDepth.onValueChanged.AddListener(delegate {
            sliderDepthToggle(sliderDepth);
        });
        sliderCataract.onValueChanged.AddListener(delegate {
            sliderCataractToggle(sliderCataract);
        });
        sliderGlaucoma.onValueChanged.AddListener(delegate {
            sliderGlaucomaToggle(sliderGlaucoma);
        });

        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        cataractScript.enabled = true;

    }

    /// <summary>
    /// Function setting initial <c>MusicVolume</c> value in <c>PlayerPrefs</c>.
    /// </summary>
    /// <param name="sliderValue"></param>
    public void SetLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        audio.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    /// <summary>
    /// Function called by yellow toggle slider.
    /// </summary>
    /// <param name="slider"></param>
    public void sliderYellowToggle(Slider slider)
    {
        switch ((int)slider.value)
        {
            case 0:
                yellowScript.enabled = false;
                yellow = false;
                break;
            case 1:
                yellow = true;
                yellowScript.enabled = true;
                break;


        } 
    }
    /// <summary>
    /// Function called by depth toggle slider.
    /// </summary>
    /// <param name="slider"></param>
    public void sliderDepthToggle(Slider slider)
    {
        switch ((int)slider.value)
        {
            case 0:
                depthScript.enabled = false;
                depth = false;
                break;
            case 1:
                depth = true;
                depthScript.enabled = true;
                break;


        }
    }
    /// <summary>
    /// Function called by cataract toggle slider.
    /// </summary>
    /// <param name="slider"></param>
    public void sliderCataractToggle(Slider slider)
    {
        switch ((int)slider.value)
        {
            case 0:
                //cataractScript.enabled = false;
                cataractScript.ChangeEnableOfCataract(false);

                cataract = false;
                break;
            case 1:
                cataract = true;
                cataractScript.ChangeEnableOfCataract(true);
                break;
        }
    }
    /// <summary>
    /// Function called by glaucoma toggle slider.
    /// </summary>
    /// <param name="slider"></param>
    public void sliderGlaucomaToggle(Slider slider)
    {
        switch ((int)slider.value)
        {
            case 0:
                glaucomaScript.enabled = false;
                glaucoma = false;
                break;
            case 1:
                glaucoma = true;
                glaucomaScript.enabled = true;
                break;
        }
    }

    private void wheelchairToggleChanged(Toggle wheelchairToggle)
    {
        ///Component wheelchairMovement = player.GetComponent("Wheelchair Movement");


        if (wheelchairToggle.isOn)
        {
            wheelchairMesh.SetActive(true);
        }
        else
        {
            wheelchairMesh.SetActive(false);
        }
    }

    /* unimplemented
    //// Update is called once per frame
    //void Update()
    //{
    //    //tectCanvas.transform.position = eyesCamera.transform.position + eyesCamera.transform.forward * 1;
    //    //architectCanvas.transform.rotation = new Quaternion(0.0f, eyesCamera.transform.rotation.y, 0.0f, eyesCamera.transform.rotation.w);
    //    //architectCanvas.transform.rotation = new Quaternion(0.0f, eyesCamera.transform.rotation.y, 0.0f, eyesCamera.transform.rotation.w);
    //}
    */

    /// <summary>
    /// Function toggling on the <c>OptionsPanel</c>.
    /// </summary>
    public void openSettings()
    {
        optionsPanel.SetActive(true);
        confirmPanel.SetActive(false);
    }

    /// <summary>
    /// Function toggling on the <c>ConfirmQuitPanel</c>.
    /// </summary>
    public void openConfirm()
    {
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    /// <summary>
    /// Function hiding the <c>OptionsPanel</c> and <c>ConfirmQuitPanel</c>.
    /// </summary>
    public void resume()
    {
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(false);
    }

    /// <summary>
    /// Function that loads the <c>sceneName</c> scene.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
