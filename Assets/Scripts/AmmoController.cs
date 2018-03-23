using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour {

    Vector3 EulerAngleVelocity;
    Rigidbody cuerpoRigido;

    void Start()
    {
        EulerAngleVelocity = new Vector3(0, 100, 0);
        cuerpoRigido = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float turn = 50f * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(EulerAngleVelocity * Time.deltaTime);
        cuerpoRigido.MoveRotation(cuerpoRigido.rotation * turnRotation);
    }

}
