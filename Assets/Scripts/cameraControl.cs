using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {

    public GameObject ship;
    private Vector3 posicionRelativa;

    private float positionYAxis = 20f;

    void Start()
    {
        posicionRelativa = transform.position - ship.transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(ship.transform.position.x, positionYAxis, ship.transform.position.z) + posicionRelativa;
    }
}
