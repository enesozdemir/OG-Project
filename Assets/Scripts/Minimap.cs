using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPostion = player.position;
        newPostion.y = transform.position.y;
        transform.position = newPostion;

        //Follow player rotation
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
