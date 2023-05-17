using UnityEngine;
using UnityEngine.AI;
    
public class MoveTo : MonoBehaviour {
    public Transform player;

    void FixedUpdate () {
        if(GetComponent<FieldOfView>().canSeePlayer)
        {
            gameObject.transform.LookAt(player.position);
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = player.position;
        }
    }
}