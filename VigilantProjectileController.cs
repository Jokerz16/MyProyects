using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigilantProjectileController : MonoBehaviour {

    Transform playerTransform;
    Rigidbody2D rb;
    public float movementSpeed;
    public LayerMask swordLayer;
    Collider2D myCol;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.Find("Character").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        myCol = GetComponent<Collider2D>();

	}
	
	// Update is called once per frame
	void Update () {

        Vector2 moveDirection = playerTransform.position - gameObject.transform.position;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        rb.velocity = transform.right*movementSpeed;
        if (myCol.IsTouchingLayers(swordLayer))
        {
            Destroy(gameObject);
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
