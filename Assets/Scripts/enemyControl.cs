using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class enemyControl : MonoBehaviour {

    public GameObject ship;

    private void followShip(bool allowFollow)
    {
        GetComponent<NavMeshAgent>().SetDestination(ship.transform.position);
    }
}
