using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    #region Singleton
    public static Ammo instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Ammo Manager!");
            return;
        }

        instance = this;
    }
    #endregion

    public int ammoAmount;
    public Text ammoAmountText;
    public GameObject gun;
    public bool isMagEmpty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoAmountText.text = ammoAmount.ToString();

        if (ammoAmount == 0)
        {
            isMagEmpty = true;
        }
    }

    public void Reload()
    {
        //Play reloding animation
        ammoAmount = 30;
        isMagEmpty = false;
    }
}
