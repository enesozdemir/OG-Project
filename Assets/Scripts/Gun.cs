using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public bool fullAutoFire;
    public Text fireType;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Ammo.instance.Reload();
        }

        if (Input.GetButton("Fire1") && fullAutoFire)
        {
            if (!Ammo.instance.isMagEmpty)
            {
                Shoot();
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            if (!Ammo.instance.isMagEmpty)
            {
                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            fullAutoFire = !fullAutoFire;
            if (fullAutoFire == true)
            {
                fullAutoFire = true;
            }
            else
            {
                fullAutoFire = false;
            }
        }

        if (fullAutoFire)
        {
            fireType.text = "Full Auto";
        }
        else
        {
            fireType.text = "Single";
        }
    }

    private void Shoot()
    {
        //MuzzleFlash Implemantaion
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            //Only Target can be damaged
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            //Bullet hit effect
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
        //countdowning bullets
        Ammo.instance.ammoAmount--;
    }
}
