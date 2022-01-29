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

    
        enemyManager.isdead = true;
        if(!enemyManager.canFly)
        {
                animator.CrossFade("Death", .1f);
                enemyManager.navMesh.isStopped = true;
        }
        else
        {
            enemyManager.GetComponent<Rigidbody>().useGravity = true;
        }
            
    }    
}
