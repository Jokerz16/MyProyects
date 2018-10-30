using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemController : MonoBehaviour {

    Transform target;
    bool up;
    float flyTimer, maxFlyTimer, attackTimer, maxAttackTimer;
    public float movementSpeed;
    Rigidbody2D rb; 

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Target").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        up = true;
        flyTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        flyTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        Vector2 moveDirection = target.position - gameObject.transform.position;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //if (flyTimer >= maxFlyTimer)
        //{

        //    rb.AddForce(transform.up, ForceMode2D.Impulse);
        //    maxFlyTimer = Random.Range(3f, 7f);
        //    flyTimer = 0;
        //}
        if (attackTimer >= maxAttackTimer)
        {
            rb.AddForce(transform.right* 3, ForceMode2D.Impulse);
            maxAttackTimer = Random.Range(2f, 4f);
            attackTimer = 0;
        }
        
           
        
    }
}
