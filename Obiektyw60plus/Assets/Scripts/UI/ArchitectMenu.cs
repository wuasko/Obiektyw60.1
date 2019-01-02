using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Assets.Scripts.EyeEffectsPostprocessing;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ArchitectMenu : MonoBehaviour {

    public Toggle wheelchairToggle;
    public GameObject wheelchairMesh;
    public GameObject eyesCamera;
    public GameObject architectCanvas;

    public GameObject architectPanel;
    public GameObject optionsPanel;
    public GameObject confirmPanel;

    public Toggle toggleYellowing;
    public Toggle toggleDepth;
    public Toggle toggleCataract;
    public Toggle toggleGlaucoma;

    public Slider volumeSlider;

    private GlaucomaEffecet glaucomaScript;
    private DepthOfField depthScript;
    private YellowEyeEffect yellowScript;
    private CataractManager cataractScript;

    public AudioMixer audio;


    // Use this for initialization
    void Start () {
        glaucomaScript = eyesCamera.GetComponent<GlaucomaEffecet>();
        depthScript = eyesCamera.GetComponent<DepthOfField>();
        yellowScript = eyesCamera.GetComponent<YellowEyeEffect>();
        cataractScript = eyesCamera.GetComponent<CataractManager>();
        architectPanel.SetActive(true);
        optionsPanel.SetActive(false);
        confirmPanel.SetActive(false);
        wheelchairToggle.onValueChanged.AddListener(delegate {
            wheelchairToggleChanged(wheelchairToggle);
        });
        toggleYellowing.onValueChanged.AddListener(delegate {
            yellowToggleChanged(toggleYellowing);
        });
        toggleDepth.onValueChanged.AddListener(delegate {
           depthToggleChanged(toggleDepth);
        });
        toggleCataract.onValueChanged.AddListener(delegate {
            cataractToggleChanged(toggleCataract);
        });
        toggleGlaucoma.onValueChanged.AddListener(delegate {
            glaucomaToggleChanged(toggleGlaucoma);
        });

        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

    }

    public void SetLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        audio.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    private void cataractToggleChanged(Toggle toggleCataract)
    {
        cataractScript.enabled = toggleCataract.isOn;
    }

    private void glaucomaToggleChanged(Toggle toggleGlaucoma)
    {
        glaucomaScript.enabled = toggleGlaucoma.isOn;
    }

    private void depthToggleChanged(Toggle toggleDepth)
    {
        depthScript.enabled = toggleDepth.isOn;
    }

    private void yellowToggleChanged(Toggle toggleYellowing)
    {
        yellowScript.enabled = toggleYellowing.isOn;
        Debug.Log("yellow go!");
    }

    private void wheelchairToggleChanged(Toggle wheelchairToggle)
    {
        if (wheelchairToggle.isOn)
        {
            wheelchairMesh.SetActive(true);
        }
        else
        {
            wheelchairMesh.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

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
