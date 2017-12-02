using UnityEngine;

public class shipMovement : MonoBehaviour {

    float torque;
    float velocidadMovimiento;
    public Rigidbody cuerpoRigido;

	void Start () {
        //cuerpoRigido = GetComponent<Rigidbody>();
        torque = 0.5F;
        velocidadMovimiento = 0.2F;
	}
	
	void FixedUpdate () {

        // Girando
        cuerpoRigido.AddTorque(transform.up * torque * Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.UpArrow))
        {
            cuerpoRigido.AddForce(transform.forward * velocidadMovimiento, ForceMode.Impulse);
            cuerpoRigido.AddTorque(Vector3.zero);
        }

    }
}
