using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{
    public Transform[] points;
    int current;
    int lastCurrent;
    public float speed;
    public AudioSource RobostepsSound;
    public Transform player;
    Animator Roboanim;

    // Start is called before the first frame update
    void Start()
    {
        lastCurrent = 0;
        current = 0;
        Roboanim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RobostepsSound.enabled = true;
        if (!GetComponent<FieldOfView>().canSeePlayer)
        {
            if(transform.position != points[current].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
            }
            else
            {
                if(lastCurrent == 0 && current != points.Length - 1)
                {
                    current = (current + 1);
                }
                else if (lastCurrent == 0 && current == points.Length - 1)
                {
                    lastCurrent = points.Length;
                    current = (current - 1);
                }
                else if (lastCurrent == points.Length && current != 0)
                {
                    current = (current - 1);
                }
                else if (lastCurrent == points.Length && current == 0)
                {
                    lastCurrent = 0;
                    current = (current + 1);
                }

            }  
                
            gameObject.transform.LookAt(points[current].position);
        }

    }

    void FixedUpdate()
    {
        if(GetComponent<FieldOfView>().canSeePlayer)
        {
            gameObject.transform.LookAt(player.position);
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = player.position;
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, Time.deltaTime/10);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Roboanim.SetTrigger("Robot Punch");
        }
    }
}
