using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SController : MonoBehaviour {

    /// <summary>
    /// Rigidbody del objeto
    /// </summary>
    Rigidbody2D myRb;

    /// <summary>
    /// Referencia a la posicion del ratón
    /// </summary>
    Transform targetTr;

    /// <summary>
    /// Referencia al script del ratón
    /// </summary>
    TargetScript targetScript;

    [Range(1f, 10f)]
    /// <summary>
    /// Velocidad de la espada
    /// </summary>
    public float movementSpeed;

    /// <summary>
    /// Referencia al transform de el objeto
    /// </summary>
    Transform myTr;

    /// <summary>
    /// Referencia al objeto Character
    /// </summary>
    GameObject character;

    /// <summary>
    /// Comprueba si la espada se ha clavado
    /// </summary>
    public bool stucked;

	// Use this for initialization
	void Start () {

        myRb = GetComponent<Rigidbody2D>();
        myTr = GetComponent<Transform>();
        character = GameObject.Find("/Game/Character");
        targetScript = GameObject.Find("/Game/Character/Target").GetComponent<TargetScript>();
    }
	
	// Update is called once per frame
	void Update () {

        //Control de rango
        //if ((myTr.position.x - character.transform.position.x) + (myTr.transform.position.y - character.transform.position.y) > character.GetComponent<PController>().range)
        //{
        //    Return(character.transform.position);
        //}
               

    }

    public void Return(Vector2 playerPos)
    {
        
        myTr.position = playerPos;
        myRb.velocity = Vector2.zero;
        character.GetComponent<PController>().swordThrowed = false;
        myRb.constraints = RigidbodyConstraints2D.None;
        stucked = false;
        gameObject.SetActive(false);
        
    }
    public void Throw(Vector3 orig, Vector3 direct)
    {
        

            myRb.velocity = (direct - orig) * movementSpeed;

            myTr.right = direct - orig;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            myRb.constraints = RigidbodyConstraints2D.FreezeAll;

            stucked = true;
        }
        else if (collision.collider.tag == "NoWall" || collision.collider.tag == "DmgWall")
        {
            Return(character.transform.position);
        }
    }
}
