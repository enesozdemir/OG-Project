using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public Transform target;
    Rigidbody rb;
    public float mermihizi;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 2f);
      
    }
    private void FixedUpdate()
    {
        rb.velocity=transform.forward*mermihizi*Time.deltaTime;
    }
}
