using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject tutorialInfo;
    public bool info;
    [Range(40f, 100f)]
    public float fovValue = 40f;
    public Camera fpsCam;
    public Camera miniMapCam;
    [Range(5f, 20f)]
    public float miniMapZoom = 12f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TutorialInfo();
        FOV();
        MinimapZoom();
    }

    public void TutorialInfo()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            info = !info;
            tutorialInfo.SetActive(info);
            if (info)
            {
                info = true;
            }
            else
            {
                info = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if(miniMapZoom<20)
             miniMapZoom += 1;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (miniMapZoom > 1)
                miniMapZoom -= 1;
        }
    }

    public void FOV()
    {
        //Buttons must be added
        fpsCam.fieldOfView = fovValue;
    }

    public void MinimapZoom()
    {
        //Buttons must be added
        miniMapCam.orthographicSize = miniMapZoom;
    }
}
