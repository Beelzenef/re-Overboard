using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private bool gameover;

    private GameObject[] enemies;

    public GameObject balaPrefab;
    public Transform spawnBalas;

    public GameObject sailorPrefab;
    public Transform spawnSailor;
    private bool jumpSide;
    private Vector3 v3Left;
    private Vector3 v3Right;

    public Text WeaponSelected;
    public Text Ammo;
    public Text Health;

    private int ammoLeft;
    private int health;
    private int maxHealth;

    private void Awake()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        enemies = GameObject.FindGameObjectsWithTag("Enemies");

        ShowOverheadView();

        StartCoroutine("CheckInput");

        paused = false;
        gameover = false;

        ammoLeft = 0;
        health = 3;
        maxHealth = 5;

        jumpSide = true;
        v3Left = new Vector3(-5, 5, 0);
        v3Right = new Vector3(5, 5, 0);

        UpdateLabels();
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
                if (Input.GetKeyDown(KeyCode.L) && ammoLeft != 0)
                {
                    Shot();
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    AbandonShip();
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

        if (gameover)
            if (GUI.Button(new Rect(10, 10, 40, 40), "Reiniciar"))
                Restart();
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

    void Shot()
    {
        GameObject bala = (GameObject)Instantiate(
            balaPrefab,
            spawnBalas.position,
            spawnBalas.rotation);

        bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 20;
        ammoLeft--;

        Destroy(bala, 2F);

        UpdateLabels();
    }

    void AbandonShip()
    {
        GameObject sailor = (GameObject)Instantiate(
            sailorPrefab,
            spawnSailor.position,
            spawnSailor.rotation);

        if (jumpSide)
        {
            sailor.GetComponent<Rigidbody>().AddForce(v3Left, ForceMode.Impulse);
        }
        else
        {
            sailor.GetComponent<Rigidbody>().AddForce(v3Right, ForceMode.Impulse);
        }
        jumpSide = !jumpSide;
            

        health--;
        UpdateLabels();
        CheckHealth();
    }

    void OnTriggerEnter(Collider c)
    {
        // Pick up health
        if ((c.gameObject.tag == "Health") && health != maxHealth)
        {
            health++;
            Destroy(c.gameObject);
            UpdateLabels();
        }

        // Pick up ammo
        if (c.gameObject.tag == "Ammo")
        {
            ammoLeft += 10;
            Destroy(c.gameObject);
            UpdateLabels();
        }

        // Gather sailors

        // Attack!
        if (c.gameObject.tag == "Enemies")
        {
            health--;
            UpdateLabels();

            CheckHealth();
        }
    }

    void CheckHealth()
    {
        if (health == 0)
            GameOver();
    }

    void UpdateLabels ()
    {
        Health.text = health.ToString();
        Debug.Log("Actualizando...");
        Ammo.text = ammoLeft.ToString();
        WeaponSelected.text = "Cañon";
    }

    void GameOver()
    {
        gameover = true;
        StopCoroutine("CheckInput");
        inputDesplazamiento = 0;
        inputGiro = 0;
    }

    void Restart()
    {
        SceneManager.LoadScene("main_tutorial");
    }


}
