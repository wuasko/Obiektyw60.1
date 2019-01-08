using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    [SerializeField]
    private GameObject directionalLight;
    [SerializeField]
    private Material daySkybox;
    [SerializeField]
    private Material nightSkybox;
    [SerializeField]
    private GameObject colorPicker;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Button SetColorButton;
    [SerializeField]
    private GameObject BedroomLights;
    [SerializeField]
    private Button LightsButton;
    [SerializeField]
    private GameObject ObjectToChangeMaterial;
    [SerializeField]
    private Color FirstColor;
    [SerializeField]
    private Color SecondColor;
    [SerializeField]
    private Color ThirdColor;

    private Material MaterialToChange;
    private Color newColor;
    private RaycastHit hit_Info;
    private GameObject selectedGameObject;
    private int setNumber;

    public void Start()
    {
        setNumber = 1;
        MaterialToChange = ObjectToChangeMaterial.GetComponent<Renderer>().sharedMaterial;
    }

    public void ToggleDirectional()
    {
        directionalLight.SetActive(!directionalLight.activeSelf);
        ToggleSkybox();
    }

    public void ToggleSkybox()
    {
        if (RenderSettings.skybox == daySkybox)
            RenderSettings.skybox = nightSkybox;
        else
            RenderSettings.skybox = daySkybox;
    }

    public void ActivateSetColorButton()
    {
        SetColorButton.gameObject.SetActive(!SetColorButton.isActiveAndEnabled);
    }

    public void ActivateColorPicker()
    {
        colorPicker.SetActive(!colorPicker.activeSelf);
        ActivateSetColorButton();
    }

    public void CheckIfInteractible()
    {
        Debug.Log("Checking if interactible");
        Debug.DrawRay(player.position, player.forward);
        Ray ray = new Ray(player.position, player.forward);

        if (Physics.Raycast(ray, out hit_Info, 100f))
        {
            if (hit_Info.transform.gameObject.GetComponent<Wall>())
            {
                ActivateColorPicker();
                selectedGameObject = hit_Info.transform.gameObject;
            }
            if (hit_Info.transform.gameObject.tag == "Light Switch")
            {
                ActivateLightSwitchButton();
                selectedGameObject = hit_Info.transform.gameObject;
            }

        }
        else
        {
            Debug.Log("Isn't interactible");
        }
    }

    public void ActivateLightSwitchButton()
    {
        LightsButton.gameObject.SetActive(!LightsButton.isActiveAndEnabled);
    }

    public void ToggleBedroomLights()
    {
        BedroomLights.SetActive(!BedroomLights.activeSelf);
    }

    public void SetColor()
    {
        Debug.Log("Setting Color");
        newColor = colorPicker.GetComponentInChildren<Image>().color;
        selectedGameObject.GetComponent<Renderer>().material.color = newColor;
    }

    public void ChangeComposition()
    {
        switch(setNumber)
            {
            case 1:
                MaterialToChange.color = SecondColor;
                setNumber++;
                break;
            case 2:
                MaterialToChange.color = ThirdColor;
                setNumber++;
                break;
            case 3:
                MaterialToChange.color = FirstColor;
                setNumber = 1;
                break;
        }
    }
}