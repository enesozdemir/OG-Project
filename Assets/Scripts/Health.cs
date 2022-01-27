using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float playerHealth = 100;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthBar.value = playerHealth;
        if (playerHealth <= 0)
        {
            //Kill the player
            Debug.Log("Health is 0");
        }
    }
}
