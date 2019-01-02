using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CataractManager : MonoBehaviour {

    private bool flaga;

	// Use this for initialization
	void Start () {
        flaga = true;
        ChangeEnableOfCataract(flaga);
	}
	
	// Update is called once per frame
	void Update () {

        //some input triiger (choose one that you want to)
        if (Input.GetKey(KeyCode.Space))
        {
            flaga = !flaga;
            ChangeEnableOfCataract(flaga);
        }
		
	}

    private void ChangeEnableOfCataract( bool bEnable)
    {
        GetComponent<RGBtoHSL>().enabled = bEnable;
        GetComponent<BlurOptimized>().enabled = bEnable;
        GetComponent<CataractEffect>().enabled = bEnable;
    }
}
