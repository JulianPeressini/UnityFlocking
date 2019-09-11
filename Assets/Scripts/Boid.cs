using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public FlockManager manager;


    private bool turning = false;
    private float speed;

    private Bounds flockArea;

    private void Start()
    {
        speed = Random.Range(manager.BoidMinSpeed, manager.BoidMaxSpeed);   
        flockArea = new Bounds(manager.transform.position, manager.Limits * 2);
    }

    private void Update()
    {
        if (!flockArea.Contains(transform.position))
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            Vector3 direction = manager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.BoidRotationSpeed * Time.deltaTime);
        }
        else
        {
            int rdm = Random.Range(0, 100);
            if (rdm < 50)
            {
                BoidBehaviour();
            }
        }

        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    private void BoidBehaviour()
    {
        GameObject[] flock;
        flock = manager.Boids;

        Vector3 avgCenter = Vector3.zero;
        Vector3 avgAvoid = Vector3.zero;
        float avgSpeed = 0;
        float neighbourDistance = 0;
        int groupSize = 0;

        foreach (GameObject boid in flock)
        {
            if (boid != this.gameObject)
            {
                neighbourDistance = Vector3.Distance(boid.transform.position, this.transform.position);
                if (neighbourDistance <= manager.BoidNeighbourDistance)
                {
                    avgCenter += boid.transform.position;
                    groupSize++;

                    if (neighbourDistance < 3.0f)
                    {
                        avgAvoid += (this.transform.position - boid.transform.position);
                    }

                    Boid otherBoid = boid.GetComponent<Boid>();
                    avgSpeed += otherBoid.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            avgCenter = avgCenter / groupSize + (manager.BoidsTarget.transform.position - this.transform.position);
            speed = avgSpeed / groupSize;

            Vector3 direction = (avgCenter + avgAvoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.BoidRotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            
        }
    }
}
