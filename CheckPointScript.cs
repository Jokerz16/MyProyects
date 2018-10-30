using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour {

    public PController pController;
    Collider2D myCol;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        myCol = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pController.ChangeSpawn(transform);
            spriteRenderer.enabled = false;
            myCol.enabled = false;
        }
        
    }
}
