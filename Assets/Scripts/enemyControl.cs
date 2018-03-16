using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class enemyControl : MonoBehaviour {

    public GameObject ship;

    bool paused;

    int puntosDeVida = 3;

    public void Awake()
    {
        paused = false;

        StartCoroutine("PlayerDetection");
    }

    IEnumerator PlayerDetection()
    {
        float maxDistanciaPermitida = 30F;

        while (true && !paused)
        {
            if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < maxDistanciaPermitida)
            {
                Debug.Log("player detected!");
                GetComponent<NavMeshAgent>().SetDestination(ship.transform.position);
            }
            yield return null;
        }
    }

    public void notifyPausedGame()
    {
        Debug.Log("Juego pausado");
        paused = !paused;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Bala")
        {
            puntosDeVida--;
            Debug.Log(puntosDeVida);
            if (puntosDeVida == 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
