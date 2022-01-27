using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject tutorialInfo;
    public bool info;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
