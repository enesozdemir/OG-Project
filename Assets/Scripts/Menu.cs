using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject tutorialInfo;
    public bool info;
    [Range(40, 100)]
    public float fovValue;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TutorialInfo();
        FOV();
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
    }

    public void FOV()
    {
        fovValue = cam.fieldOfView;
    }
}
