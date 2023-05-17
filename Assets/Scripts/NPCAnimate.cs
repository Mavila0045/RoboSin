using UnityEngine;
using UnityEngine.AI;

public class NPCAnimate : MonoBehaviour
{
    public Transform Pplayer;
    Animator NPCanim;

    void Start()
    {
        NPCanim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (GetComponent<FieldOfView>().canSeePlayer)
        {
            gameObject.transform.LookAt(Pplayer.position);
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = Pplayer.position;

            NPCanim.SetTrigger("NPC Walk");

        }
        else
        {
            NPCanim.SetTrigger("NPC Idle");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            NPCanim.SetTrigger("NPC Idle");
            for (int i = 0; i < 1000; i++)
            {

            }
        }
    }
}