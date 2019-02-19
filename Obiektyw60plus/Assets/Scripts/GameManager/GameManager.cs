using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    /// <summary>
    /// Attaching directionalLight to toggle day/night
    /// </summary>
    [SerializeField]
    private GameObject directionalLight;
    /// <summary>
    /// Attaching skybox to toggle day/night
    /// </summary>
    [SerializeField]
    private Material daySkybox;
    /// <summary>
    /// Attaching nightskybox to toggle day/night
    /// </summary>
    [SerializeField]
    private Material nightSkybox;
    /// <summary>
    /// Attaching colorpicker enabling it from the menu
    /// </summary>
    [SerializeField]
    private GameObject colorPicker;
    /// <summary>
    /// Attaching player transform to raycast in the forward directory
    /// </summary>
    [SerializeField]
    private Transform player;
    /// <summary>
    /// Button activated as colorpicker appears
    /// </summary>
    [SerializeField]
    private Button SetColorButton;
    /// <summary>
    /// Attaching bedroom lights to toggling it when pressing the lightswitch
    /// </summary>
    [SerializeField]
    private GameObject BedroomLights;
    /// <summary>
    /// Attaching lightswitch to toggle the lights in the bedroom
    /// </summary>
    [SerializeField]
    private Button LightsButton;
    /// <summary>
    /// Attaching the object which has the material that we're switching between sets of colors
    /// </summary>
    [SerializeField]
    private GameObject ObjectToChangeMaterial;
    /// <summary>
    /// Attaching the object which has the material that we're switching between sets of colors
    /// </summary>
    [SerializeField]
    private GameObject SecondObjectToChangeMaterial;
    /// <summary>
    /// Attaching colors of the sets
    /// </summary>
    [SerializeField]
    private Color FirstColor;
    [SerializeField]
    private Color SecondColor;
    [SerializeField]
    private Color ThirdColor;
    [SerializeField]
    private Color FourthColor;
    [SerializeField]
    private Color FifthColor;
    [SerializeField]
    private Color SixthColor;

    /// <summary>
    /// Attaching the object which has the material that we're switching between sets of colors
    /// </summary>
    private Material MaterialToChange;
    private Material SecondMaterialToChange;
    private Color newColor;
    private RaycastHit hit_Info;
    private GameObject selectedGameObject;

    private int setNumber;

    public void Start()
    {
        setNumber = 1;      //Setting the current set of colours as first
        /// <summary>
        /// Getting the material that we want to switch
        /// </summary>
        MaterialToChange = ObjectToChangeMaterial.GetComponent<Renderer>().sharedMaterial;
        /// <summary>
        /// Getting the material that we want to switch
        /// </summary>
        SecondMaterialToChange = SecondObjectToChangeMaterial.GetComponent<Renderer>().sharedMaterial;
    }

    /// <summary>
    /// Function that's disabling/enabling directional light and calling function that toggles the skybox
    /// </summary>
    public void ToggleDirectional()
    {
        directionalLight.SetActive(!directionalLight.activeSelf);
        ToggleSkybox();
    }

    /// <summary>
    /// Function that's that toggles the skybox
    /// </summary>
    public void ToggleSkybox()
    {
        if (RenderSettings.skybox == daySkybox)
            RenderSettings.skybox = nightSkybox;
        else
            RenderSettings.skybox = daySkybox;
    }

    /// <summary>
    /// Function that's disabling/enabling the button to set the color to an object
    /// </summary>
    public void ActivateSetColorButton()
    {
        SetColorButton.gameObject.SetActive(!SetColorButton.isActiveAndEnabled);
    }

    /// <summary>
    /// Function that's disabling/enabling the colorpicker
    /// </summary>
    public void ActivateColorPicker()
    {
        colorPicker.SetActive(!colorPicker.activeSelf);
        ActivateSetColorButton();
    }

    /// <summary>
    /// Function which checks if an object is interactible, if is than assigning it to selectedGameObject
    /// if the object is a Wall it activates the color picker, if it's the light switch it activates the ligth switch button
    /// </summary>
    public void CheckIfInteractible()
    {
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
            //Debug.Log("Isn't interactible");
        }
    }

    /// <summary>
    /// Function that's disabling/enabling light switch button
    /// </summary>
    public void ActivateLightSwitchButton()
    {
        LightsButton.gameObject.SetActive(!LightsButton.isActiveAndEnabled);
    }

    /// <summary>
    /// Function that's toggling the bedroom lights
    /// </summary>
    public void ToggleBedroomLights()
    {
        BedroomLights.SetActive(!BedroomLights.activeSelf);
    }

    /// <summary>
    /// Function that is called when you press the set color button
    /// </summary>
    public void SetColor()
    {
        newColor = colorPicker.GetComponentInChildren<Image>().color;
        selectedGameObject.GetComponent<Renderer>().material.color = newColor;
    }

    /// <summary>
    /// Function that changes the whole set of colors in the flat
    /// </summary>
    public void ChangeComposition()
    {
        switch(setNumber)
            {
            case 1:
                MaterialToChange.color = SecondColor;
                SecondMaterialToChange.color = FifthColor;
                setNumber++;
                break;
            case 2:
                MaterialToChange.color = ThirdColor;
                SecondMaterialToChange.color = SixthColor;
                setNumber++;
                break;
            case 3:
                MaterialToChange.color = FirstColor;
                SecondMaterialToChange.color = FourthColor;
                setNumber = 1;
                break;
        }
    }
}