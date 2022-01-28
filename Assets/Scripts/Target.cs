using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 100f;
    Animator animator;
    EnemyManager enemyManager;
    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        animator = GetComponentInChildren<Animator>();
    }
    public void TakeDamage(float amount)
    {

        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.CrossFade("Death", .1f);
        enemyManager.isdead = true;
        if(!enemyManager.canFly)
        enemyManager.navMesh.isStopped = true;
        else
        {
            enemyManager.GetComponent<Rigidbody>().useGravity = true;
        }
            
    }    
}
