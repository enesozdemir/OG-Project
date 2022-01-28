using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    NavMeshAgent navMesh;
    Transform Player;
    EnemyStats enemyStats;
    public LayerMask detectionLayer;
    bool isRecognize;
    bool canFire;
    Rigidbody rb;
    public Transform aim;
    public GameObject bulletPrefab;
    public float timer;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyStats = GetComponent<EnemyStats>();
        navMesh = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
         if(Player!=null)
        {
            if(!isRecognize)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, enemyStats.catchRecognize, detectionLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    PlayerMovement characterStats = colliders[i].transform.GetComponent<PlayerMovement>();
                    if (characterStats != null)
                    {
                        Vector3 targetDirection = characterStats.transform.position - transform.position;
                        float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);
                        if (viewAbleAngle > enemyStats.minimumDetectionAngle && viewAbleAngle < enemyStats.maximumDetectionAngle)
                        {
                            isRecognize = true;
                        }
                    }

                }
            }
            else if(isRecognize&&!canFire)
            {

                navMesh.isStopped = false;
                Vector3 relativeDirection = transform.InverseTransformDirection(navMesh.desiredVelocity);
                navMesh.enabled = true;
                navMesh.SetDestination(Player.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, navMesh.transform.rotation,enemyStats.rotationSpeed / Time.deltaTime);

                Collider[] colliders = Physics.OverlapSphere(transform.position, enemyStats.fireRecognize, detectionLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    PlayerMovement characterStats = colliders[i].transform.GetComponent<PlayerMovement>();
                    if (characterStats != null)
                    {
                        Vector3 targetDirection = characterStats.transform.position - transform.position;
                        float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);
                        if (viewAbleAngle > enemyStats.minimumDetectionAngle && viewAbleAngle < enemyStats.maximumDetectionAngle)
                        {
                            canFire = true;
                        }
                    }
                }
                if(colliders.Length==0)
                {
                    isRecognize = false;
                }

            }
            if(isRecognize&&canFire)
            {
                transform.LookAt(Player);
                navMesh.isStopped = true;
                timer += Time.deltaTime;
                RaycastHit hit;
                if(Physics.Raycast(aim.position,aim.forward,out hit,enemyStats.fireRecognize))
                {
                    if(timer>enemyStats.mermiFrekans)
                    Fire();
                }
                Vector3 relativeDirection = transform.InverseTransformDirection(navMesh.desiredVelocity);
                navMesh.enabled = true;
                navMesh.SetDestination(Player.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, navMesh.transform.rotation, enemyStats.rotationSpeed / Time.deltaTime);

                Collider[] colliders = Physics.OverlapSphere(transform.position, enemyStats.fireRecognize, detectionLayer);
                for (int i = 0; i < colliders.Length; i++)
                {
                    PlayerMovement characterStats = colliders[i].transform.GetComponent<PlayerMovement>();
                    if (characterStats != null)
                    {
                        Vector3 targetDirection = characterStats.transform.position - transform.position;
                        float viewAbleAngle = Vector3.Angle(targetDirection, transform.forward);
                        if (viewAbleAngle > enemyStats.minimumDetectionAngle && viewAbleAngle < enemyStats.maximumDetectionAngle)
                        {
                            canFire = true;
                        }
                    }
                }
                if (colliders.Length == 0)
                {
                    canFire = false;
                }
            }


        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, aim.position, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z));
        bullet.GetComponentInChildren<Bullet>().mermihizi = enemyStats.mermihizi;
        timer = 0;
        
    }
}
