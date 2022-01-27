using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    #region Singleton
    public static Stamina instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Stamina Manager!");
            return;
        }

        instance = this;
    }
    #endregion

    public float stamina;
    public float maxStamina = 100;
    public float number;
    public Slider staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateStamina();

        staminaBar.value = stamina;
        if (stamina < 0)
        {
            stamina = 0;
        }
    }

    public void RegenerateStamina()
    {
        if (stamina < maxStamina)
        {
            stamina += 1f * Time.deltaTime;
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }
    }
}
