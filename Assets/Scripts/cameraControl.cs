using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {

    public GameObject ship;
    private Vector3 posicionRelativa;

    void Start()
    {
        posicionRelativa = transform.position - ship.transform.position;
    }

    void LateUpdate()
    {
        transform.position = ship.transform.position + posicionRelativa;
    }
}
