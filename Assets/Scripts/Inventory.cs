using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Inventory Manager!");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int space = 21;
    public List<Item> items = new List<Item>();
    public Canvas inventoryCanvas;
    public GameObject mouseLook;
    public GameObject gunControl;

    public bool Add (Item item)
    {
        if (!item.isDefeaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("No space in Inventory");
                return false;
            }

            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
        }

        return true;
    }

    public void Drop (Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryCanvas.rootCanvas.enabled = !inventoryCanvas.rootCanvas.enabled;
            if (inventoryCanvas.rootCanvas.enabled)
            {
                inventoryCanvas.rootCanvas.enabled = true;
                Cursor.lockState = CursorLockMode.Confined;
                mouseLook.GetComponent<MouseLook>().enabled = false;
                gunControl.GetComponent<Gun>().enabled = false;
            }
            else
            {
                inventoryCanvas.rootCanvas.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                mouseLook.GetComponent<MouseLook>().enabled = true;
                gunControl.GetComponent<Gun>().enabled = true;
            }
        }        
    }


}
