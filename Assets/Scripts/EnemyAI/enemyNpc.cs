using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyNpc : MonoBehaviour
{
    public List<Transform> points;
    NavMeshAgent navMesh;
    Vector3 currentPosition;
    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(points[Random.Range(0, points.Count)].position);
    }
    private void Update()
    {
       if(navMesh.remainingDistance<3f)
        {
            navMesh.SetDestination(points[Random.Range(0, points.Count)].position);
        }
    }
}
