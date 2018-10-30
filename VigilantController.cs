using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigilantController : MonoBehaviour {

    Collider2D myCol, areaCol;
    GameManager gameManager;
    Animator eyeAnim;
    public GameObject vigilantProjectile, spawnPoint1, spawnPoint2;
    public LayerMask playerLayer;
    float attackTimer, maxAttackTimer;
    public int health;

	// Use this for initialization
	void Start () {
        myCol = GetComponent<Collider2D>();
        eyeAnim = GameObject.Find("/Vigilante/Eye").GetComponent<Animator>();
        areaCol = GameObject.Find("/Vigilante/AreaVigilante").GetComponent<Collider2D>();
        attackTimer = 0;
        maxAttackTimer = Random.Range(1.5f, 5f);

	}
	
	// Update is called once per frame
	void Update () {
        attackTimer += Time.deltaTime;
        eyeAnim.SetBool("Detected", false);
        if (areaCol.IsTouchingLayers(playerLayer))
        {
            eyeAnim.SetBool("Detected", true);
            if (attackTimer >= maxAttackTimer)
            {
                Instantiate(vigilantProjectile, spawnPoint1.transform.position, Quaternion.identity);
                Instantiate(vigilantProjectile, spawnPoint2.transform.position, Quaternion.identity);
                attackTimer = 0f;
                maxAttackTimer = Random.Range(1.5f, 5f);
            }
            
            
        }
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }

	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            health--;
        }
    }
}
