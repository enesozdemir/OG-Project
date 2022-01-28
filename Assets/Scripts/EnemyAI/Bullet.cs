using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public Vector3 dir;
    Rigidbody rb;
    public float mermihizi;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 2f);
      
    }
    private void FixedUpdate()
    {
        rb.velocity=-dir*mermihizi*Time.deltaTime;
    }
}
