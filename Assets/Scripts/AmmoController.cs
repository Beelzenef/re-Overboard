using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour {

    Rigidbody cuerpoRigido;

    void Start()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float turn = 50f * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        cuerpoRigido.MoveRotation(cuerpoRigido.rotation * turnRotation);
    }

}
