using System.Collections;
using UnityEngine;

public class shipMovement : MonoBehaviour {

    private Rigidbody cuerpoRigido;

    public float velocidadMovimiento = 12f;                
    public float velocidadGiro = 180f;

    private float inputDesplazamiento;       
    private float inputGiro;

    public Camera firstPersonCamera;
    public Camera overheadCamera;

    private Rect sizeVentana = new Rect(15, 15, 200, 150);

    private bool paused;

    private GameObject[] enemies;

    private void Awake()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        enemies = GameObject.FindGameObjectsWithTag("Enemies");

        ShowOverheadView();

        StartCoroutine("CheckInput");

        paused = false;
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            Move();
            Turn();
        }
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
            if (!paused)
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
                if (Input.GetKeyDown(KeyCode.M))
                {
                    paused = true;

                    foreach (GameObject item in enemies)
                    {
                        GameObject.FindGameObjectWithTag("Enemies").SendMessage("notifyPausedGame");
                    }
                }
            }

            yield return null;
        }
    }

    void OnGUI()
    {
        if (paused)
            sizeVentana = GUI.Window(0, sizeVentana, moverVentana, "Juego en pausa");
    }

    private void moverVentana(int id)
    {
        if (GUI.Button(new Rect(sizeVentana.width * 0.5f - 70, sizeVentana.width * 0.5f - 80, 150, 20),
            "Continuar"))
        {
            paused = false;
        }
        if (GUI.Button(new Rect(sizeVentana.width * 0.5f - 70, sizeVentana.width * 0.5f - 50, 150, 20),
            "Intrucciones"))
        {
            System.Diagnostics.Process.Start("https://github.com/Beelzenef/re-Overboard/wiki/Instrucciones");
        }
        if (GUI.Button(new Rect(sizeVentana.width * 0.5f - 70, sizeVentana.width * 0.5f - 20, 150, 20),
             "Salir"))
        {
            Application.Quit();
        }

        GUI.DragWindow();
    }

}
