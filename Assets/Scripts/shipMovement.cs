using System.Collections;
using UnityEngine;

public class shipMovement : MonoBehaviour {

    private Rigidbody cuerpoRigido;

    public float velocidadMovimiento = 12f;                
    public float velocidadGiro = 180f;

    private float inputDesplazamiento;       
    private float inputGiro;

    private GameObject[] enemies;

    public Camera firstPersonCamera;
    public Camera overheadCamera;

    private void Awake()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        enemies = GameObject.FindGameObjectsWithTag("Enemies");

        ShowOverheadView();

        StartCoroutine("CheckNearEnemies");
        StartCoroutine("CheckInput");
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

    // Camera mng

    public void ShowOverheadView()
    {
        firstPersonCamera.enabled = false;
        overheadCamera.enabled = true;
    }

    public void ShowFirstPersonView()
    {
        firstPersonCamera.enabled = true;
        overheadCamera.enabled = false;
    }

    IEnumerator CheckInput()
    {
        while (true)
        {
            inputDesplazamiento = Input.GetAxis("Vertical");
            inputGiro = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.P))
            {
                ShowFirstPersonView();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                ShowOverheadView();
            }

            yield return null;
        }
    }

    IEnumerator CheckNearEnemies()
    {
        float maxDistanciaPermitida = 30F;

        while (true)
        {
            foreach (GameObject item in enemies)
            {
                if (Vector3.Distance(transform.position, item.transform.position) < maxDistanciaPermitida)
                {
                    Debug.Log("Enemigo cercano");
                }
            }
            yield return null;
        }
    }
}
