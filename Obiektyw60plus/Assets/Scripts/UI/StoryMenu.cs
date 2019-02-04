using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Assets.Scripts.EyeEffectsPostprocessing;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
/// <summary>
/// Class <c>StoryMenu</c>, responsible for user interface in story mode.
/// </summary>
public class StoryMenu : MonoBehaviour
{
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
    private YellowEyeEffect yellowScript;
    private CataractManager cataractScript;

    public AudioMixer audio;

    private bool yellow, depth, glaucoma, cataract;
    private bool pause;


    /// <summary>
    /// Class <c>StoryMenu</c> initialization
    /// </summary>
    void Start()
    {
        pause = false;
        yellow = false;
        depth = false;
        glaucoma = false;
        cataract = false;
        glaucomaScript = eyesCamera.GetComponent<GlaucomaEffecet>();
        depthScript = eyesCamera.GetComponent<DepthOfField>();
        yellowScript = eyesCamera.GetComponent<YellowEyeEffect>();
        cataractScript = eyesCamera.GetComponent<CataractManager>();
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(false);

        cataractScript.enabled = true;
 
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


    public void yellowToggle()
    {

        if (yellow)
        {
            yellowScript.enabled = false;
            yellow = false;
        }
        else
        {
            yellow = true;
            yellowScript.enabled = true;
        }
    }
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
    public void depthToggle()
    {
        if (depth)
        {
            depth = false;
            depthScript.enabled = false;
        }
        else
        {
            depth = true;
            depthScript.enabled = true;
        }
    }
    public void glaucomaToggle()
    {
        if (glaucoma)
        {
            glaucoma = false;
            glaucomaScript.enabled = false;
        }
        else
        {
            glaucoma = true;
            glaucomaScript.enabled = true;
        }
    }
    public void cataractToggle()
    {
        if (glaucoma)
        {
            cataract = false;
            cataractScript.enabled = false;
        }
        else
        {
            cataract = true;
            cataractScript.enabled = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Pause game
        if (OVRInput.Get(OVRInput.Button.Two) || (Input.GetKeyDown(KeyCode.Space)))
        {
            if (pause)
            {
                pause = false;
                resume();
            }
            else
            {
                pause = true;
                openSettings();
            }
            //StartCoroutine(Wait());
        }
        //architectCanvas.transform.position = eyesCamera.transform.position + eyesCamera.transform.forward * 1;
        //architectCanvas.transform.rotation = new Quaternion(0.0f, eyesCamera.transform.rotation.y, 0.0f, eyesCamera.transform.rotation.w);
        //architectCanvas.transform.rotation = new Quaternion(0.0f, eyesCamera.transform.rotation.y, 0.0f, eyesCamera.transform.rotation.w);
    }

    //IEnumerator Wait()
    //{
    //    print(Time.time);
    //    yield return new WaitForSeconds(1);
    //    print(Time.time);
    //}

    public void openSettings()
    {
        optionsPanel.SetActive(true);
        confirmPanel.SetActive(false);
    }

    public void openConfirm()
    {
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    public void resume()
    {
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
