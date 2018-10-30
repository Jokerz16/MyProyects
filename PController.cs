using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PController : MonoBehaviour {


    /// <summary>
    /// RigidBody del objeto
    /// </summary>
    private Rigidbody2D my_Rb;

    /// <summary>
    /// Collider2D del objeto
    /// </summary>
    private Collider2D my_Col;

    /// <summary>
    /// Sprite del objeto
    /// </summary>
    private SpriteRenderer my_Sprite;

    /// <summary>
    /// Referencia a la Layer Wall
    /// </summary>
    private LayerMask wall_Layer;
    /// <summary>
    /// Layer del suelo
    /// </summary>
    private LayerMask ground_Layer;

    /// <summary>
    /// Referencia a la espada
    /// </summary>
    private GameObject my_Sword;

    [HideInInspector]
    /// <summary>
    /// Referencia al transform del cursor
    /// </summary>
    private Transform target_Tr;

    /// <summary>
    /// Referencia al transform del Spawn
    /// </summary>
    private Transform spawnPoint;
    /// <summary>
    /// Referencia al transform del objeto
    /// </summary>
    Transform myTr;

    /// <summary>
    /// Referencia al script de la espada
    /// </summary>
    SController s_Controller;

    /// <summary>
    /// Referencia al animator del objeto
    /// </summary>
    Animator myAnim;

    /// <summary>
    /// Referencia al texto del score
    /// </summary>
    private Text scoreText;

    [Range(0, 1)]
    /// <summary>
    /// Timer del lanzamiento de la espada
    /// </summary>
    private float swordCd;
    /// <summary>
    /// Almacenador de tiempo
    /// </summary>
    private float scoreTime;
    [Range(1f, 100f)]
    /// <summary>
    /// Velocidad de movimiento horizontal
    /// </summary>
    private float movementVelocity;
    [Range(1, 100000)]
    /// <summary>
    /// Potencia de subida
    /// </summary>
    private float jumpSpeed;
    /// <summary>
    /// Offset horizontal del RaycastHit2D
    /// </summary>
    private float horRayOffset;
    /// <summary>
    /// Offset vertical del RaycastHit2D
    /// </summary>
    private float verRayOffset;
    [Range(0, 20)]
    /// <summary>
    /// Alcance máximo del salto
    /// </summary>
    private float maxJumpSpeed;

    /// <summary>
    /// Verifica si está o no tocando el suelo
    /// </summary>
    public bool grounded;
    /// <summary>
    /// Comprueba si se ha lanzado o no la espada
    /// </summary>
    public bool swordThrowed;
    /// <summary>
    /// Comprueba si está tocando o no una pared
    /// </summary>
    public bool walled;



    /// <summary>
    /// Vector2 que almacena la dirección hacia la que se lanzará el RaycastHit2D
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// Almacena el origen desde que se lanzará el RaycastHit2D
    /// </summary>
    Vector3 origin;



    /// <summary>
    /// Saltos acutales
    /// </summary>
    public byte currentJumps;

    /// <summary>
    /// Saltos máximos
    /// </summary>
    public byte maxJumps;

    [Tooltip("Cuanto más alto sea, más lento caerá mientras toca la pared")]
    [Range(5,20)]
    /// <summary>
    /// Drag del rigidbody del objeto
    /// </summary>
    public int drag;

    /// <summary>
    /// Almacena los puntos
    /// </summary>
    int score;

    /// <summary>
    /// Almacena las muertes
    /// </summary>
    int deathsCounter;

    bool moving;
    float fallingSpeed;

    /// <summary>
    /// Almacenamiento del componente texto del tutorial
    /// </summary>
    public GameObject tutorialText;

    /// <summary>
    /// Almacenamiento de los distintos textos del tutorial
    /// </summary>
    string[] tutorial = new string[] {"¡Bienvenid@!\n\nPara empezar, prueba a moverte horizontalmente con las teclas A y D.\n\nPara saltar, utiliza la tecla W ¡Tienes hasta dos saltos!",
                                      "Este orbe que gira es un CHECKPOINT, cuando mueras, volverás a la posición del último que hayas recogido, a no ser que recojas otro claro.\n\nEl bloque ROJO que hay más adelante, es un bloque de daño, si pisas uno, o te chocas con él, volverás al 'Checkpoint' y sumarás una muerte a tu seguimiento.\n\nMás adelante se encuentra un coleccionable, estos OJOS estarán escondidos por el mapa y si los recoges, aumentará tu puntuación.\n\nIntenta recogerlo y ¡ten cuidado!",
                                      "¡Genial!\nAhora viene algo más complicado, la espada.\n\nSi te fijas, en el techo hay unos cuantos bloques verdes, en ellos, tu espada se quedará clavada y podrás teletransportarte hacia ella.\nPrueba a apuntar a alguno de ellos y a lanzar la espada con el CLICK IZQUIERDO de tu ratón.Si vuelves a pulsarlo, recuperarás la espada.\n\nPara teletransportarte a su posición pulsa el CLICK DERECHO, cuando te teletransportas, recargas uno de tus saltos.",
                                      "¡Perfecto!\n\nPor último, TODOS los bloques AZULES repelerán la espada y ésta volverá a ti.\n\nIntenta alcanzar el bloque verde y pasar al otro lado.",
                                      "¡Pues ya estaría!\n\nUn poquito más adelante empieza el nivel, procederé a reiniciar los marcadores y ¡a jugar!.\n\nEnjoy!" };
    /// <summary>
    /// Contador del tutorial
    /// </summary>
    int tutorialCounter;

    /// <summary>
    /// Comprobador para reiniciar los marcadores 
    /// </summary>
    bool restart;
    public GameObject wall, temporalWall;
    bool tped;
    float tpedTimer;
    public bool tut;
    public GameObject pause, stuckedObject;
    public BoxCollider2D hands;

    // Use this for initialization
    void Start() {

        my_Sword = GameObject.Find("/Game/Character/Sword");
        target_Tr = GameObject.Find("/Game/Character/Target").GetComponent<Transform>();
        spawnPoint = GameObject.Find("/Game/Scenary/SpawnPoints/SpawnPoint").GetComponent<Transform>();
        scoreText = GameObject.Find("/Canvas/ScoreText").GetComponent<Text>();
        pause = GameObject.Find("/Canvas/Pause");
        myTr = GetComponent<Transform>();
        myAnim = GetComponent<Animator>();
        my_Rb = GetComponent<Rigidbody2D>();
        my_Col = GetComponent<Collider2D>();
        my_Sprite = GetComponent<SpriteRenderer>();
        my_Sword.SetActive(false);
        temporalWall = gameObject;
        scoreTime = 0;
        deathsCounter = 0;
        if (tut)
        { 
        tutorialText.GetComponent<Text>().text = tutorial[0];
        
        tutorialCounter = 0;

        }
        else
        {
            tutorialText.gameObject.SetActive(false);
        }
        restart = false;
        pause.SetActive(false);
        stuckedObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        myAnim.SetFloat("Velocity", my_Rb.velocity.x);
        PlayerMovement();
        PlayerSkills();

        scoreText.text = "Time: " + scoreTime.ToString("N2")+ " seconds" + "\nScore: " + score + "\nYou died "+ deathsCounter + " times!"; 
        scoreTime += Time.deltaTime;
        if (restart)
        {
            scoreTime = 0;
            score = 0;
            deathsCounter = 0;
            tutorialText.SetActive(false);
            restart = false;
        }
        if (tped && tpedTimer < 0.15f)
        {
            tpedTimer += Time.deltaTime;
        }
        else { tped = false; }
        if (!tped)
        {
            my_Rb.constraints = RigidbodyConstraints2D.None;
            my_Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        stuckedObject.SetActive(my_Sword.GetComponent<SController>().stucked);

    }


    /// <summary>
    /// Control del movimiento del jugador
    /// </summary>
    void PlayerMovement()
    {
        Vector3 easeVelocity = my_Rb.velocity;

        easeVelocity.y = my_Rb.velocity.y;

        easeVelocity.z = 0f;

        easeVelocity.x *= 0.001f;

        grounded = my_Col.IsTouchingLayers(ground_Layer);
        walled = my_Col.IsTouchingLayers(wall_Layer);


        if (grounded)
        {
            currentJumps = 0;
            my_Rb.velocity = easeVelocity;
        }
        else
        {

            my_Rb.velocity = new Vector2(easeVelocity.x * 10, my_Rb.velocity.y);

        }

        if (Input.GetKey(KeyCode.A) && !moving)
        {

            my_Rb.velocity = new Vector2(-movementVelocity, my_Rb.velocity.y);
            my_Sprite.flipX = true;
            horRayOffset = -11f;
            moving = true;

        }
        else { moving = false; }

        if (Input.GetKey(KeyCode.D) && !moving)
        {
            my_Rb.velocity = new Vector2(movementVelocity, my_Rb.velocity.y);
            my_Sprite.flipX = false;
            horRayOffset = 11f;
            moving = true;
        }
        else { moving = false; }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && currentJumps < maxJumps)
        {
            my_Rb.velocity = new Vector2(my_Rb.velocity.x, 0);
            currentJumps++;
            my_Rb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);

        }

        if (!grounded && Input.GetKeyDown(KeyCode.S))
        {
            fallingSpeed = my_Rb.velocity.y;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if (walled && !grounded)
        {
            my_Rb.drag = drag;
            if (Input.GetKey(KeyCode.W))
            {
                my_Rb.AddForce(new Vector2(0f, jumpSpeed * 0.2f), ForceMode2D.Impulse);
                currentJumps = 0;
            }



            //if (wallTiming == null)
            //{
            //    wallTiming = StartCoroutine(WallTimer(0.25f));
            //}
        }
        else { my_Rb.drag = 1; }


    }

    /// <summary>
    /// Habilidades del jugador
    /// </summary>
    void PlayerSkills()
    {
        myAnim.SetBool("Throwing", false);

        #region Lanzamiento de la espada



        if (Input.GetMouseButtonDown(0) && !swordThrowed)
        {
            myAnim.SetBool("Throwing", true);
            CastRay();

        }
        else if(Input.GetMouseButtonDown(0) && swordThrowed )
        {
            my_Sword.GetComponent<SController>().Return(myTr.position);
            my_Sword.SetActive(false);

            swordThrowed = false;
            
        }

        if(Input.GetMouseButtonDown(1) && swordThrowed && my_Sword.GetComponent<SController>().stucked)
        {
            TpSword();
        }
       
        #endregion

    }

    /// <summary>
    /// Casteo del Raycast2D y llamada a la funcion Throw() del script de la espada 
    /// </summary>
    void CastRay()
    {

        RaycastHit2D []hit;

        direction = target_Tr.position - transform.position;
        origin = transform.position;
        hit = Physics2D.RaycastAll( origin, direction);
      
        
       if(hit.Length >= 2 )
        {
                
        my_Sword.SetActive(true);
        swordThrowed = true;
        my_Sword.GetComponent<SController>().Throw(origin, hit[1].point);
            
        }

    }

    void TpSword()
    {
        my_Rb.constraints = RigidbodyConstraints2D.FreezeAll;
        tped = true;
        tpedTimer = 0;
        myTr.position = new Vector2(my_Sword.transform.position.x, my_Sword.transform.position.y )  ;
        my_Sword.GetComponent<SController>().Return(myTr.position);
        my_Rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "DmgWall")
        {

            myTr.position = spawnPoint.position;
            my_Sword.GetComponent<SController>().Return(myTr.position);
            deathsCounter++;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tutorial" && tutorialCounter < 4 )
        {
            tutorialCounter++;
            tutorialText.GetComponent<Text>().text = tutorial[tutorialCounter];
            Destroy(collision.gameObject);
            
        }
        if(collision.tag == "Restart")
        {
            restart = true;
            Destroy(collision.gameObject);
        }
    }

    public void ChangeSpawn(Transform newSpawn)
    {
        spawnPoint.transform.position = newSpawn.position;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }
}
