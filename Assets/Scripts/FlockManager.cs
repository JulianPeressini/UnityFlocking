using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    
    [SerializeField] private GameObject boidPrefab;
    private GameObject[] boids;

    [SerializeField] int boidQuantity;
    [SerializeField] private Vector3 spawnArea;
    [SerializeField] private Vector3 limits;


    [SerializeField] private float boidMinSpeed;
    [SerializeField] private float boidMaxSpeed;
    [SerializeField] private float boidRotationSpeed;
    [SerializeField] private float boidNeighbourDistance;
    [SerializeField] private GameObject boidsTarget;

    public GameObject[] Boids { get { return boids; } }
    public GameObject BoidsTarget { get { return boidsTarget; } }
    public Vector3 Limits { get { return limits; } }
    public float BoidMinSpeed {get { return boidMinSpeed; } }
    public float BoidMaxSpeed { get { return boidMaxSpeed; } }
    public float BoidRotationSpeed { get { return boidRotationSpeed; } }
    public float BoidNeighbourDistance { get { return boidNeighbourDistance; } }

    void Start()
    {
        boids = new GameObject[boidQuantity];
        
        for (int i = 0; i < boidQuantity; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), Random.Range(-spawnArea.z, spawnArea.z));

            boids[i] = (GameObject) Instantiate(boidPrefab, pos, Quaternion.identity);
            boids[i].transform.SetParent(gameObject.transform);
            boids[i].GetComponent<Boid>().manager = this;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnArea * 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, limits * 2);
    }

}
