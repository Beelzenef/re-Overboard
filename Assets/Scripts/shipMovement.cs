using UnityEngine;

public class shipMovement : MonoBehaviour {

    private Rigidbody cuerpoRigido;

    public float velocidadMovimiento = 12f;                
    public float velocidadGiro = 180f;

    private float inputDesplazamiento;       
    private float inputGiro;       


    private void Awake()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputDesplazamiento = Input.GetAxis("Vertical");
        inputGiro = Input.GetAxis("Horizontal"); 
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }


    private void Move()
    {
        Vector3 movement = transform.forward * inputDesplazamiento * velocidadMovimiento * Time.deltaTime;

        cuerpoRigido.MovePosition(cuerpoRigido.position + movement);
    }


    private void Turn()
    {
        float turn = inputGiro * velocidadGiro * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        cuerpoRigido.MoveRotation(cuerpoRigido.rotation * turnRotation);
    }
}
